using System;
using System.Data;
using System.Reflection;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using Utility.ExcelReader;

public static class RowDataEx
{
    private const string SplitSignature = ":";
    private const string RowDataConvertingMethodName = "FromRowData";
    public static SerializedDictionary<string, TValueObject> ToObjects<TValueObject>(this ExcelSheetInfo sheetInfo)
    {
        SerializedDictionary<string, TValueObject> result = new SerializedDictionary<string, TValueObject>(sheetInfo.RowDataDict.Values.Count);
        foreach (RowData rowData in sheetInfo.RowDataDict.Values)
        {
            TValueObject instance = rowData.ToObject<TValueObject>();
            if (instance is null) continue;
            result.Add(rowData.ObjectName, instance);
        }
        return result;
    }
    public static SerializedDictionary<string, object> ToObjects(this ExcelSheetInfo sheetInfo, string type)
    {
        Type instanceType = Type.GetType(type);
        if (instanceType is null) throw new Exception($"Type is null : {type}");
        SerializedDictionary<string, object> result = new SerializedDictionary<string, object>(sheetInfo.RowDataDict.Values.Count);
        foreach (RowData rowData in sheetInfo.RowDataDict.Values)
        {
            object instance = rowData.ToObject(type);
            result.Add(rowData.ObjectName, instance);
        }
        return result;
    }
 
    public static TValueObject ToObject<TValueObject>(this RowData rowData)
    {
        TValueObject instance = Activator.CreateInstance<TValueObject>();
        Type excelWriterType = instance.GetType().GetInterface("IFromRowData");
        if(excelWriterType is null)
        {
            rowData.SetPropertiesValueIfExist(instance);
            rowData.SetFieldsValueIfExist(instance);
        }
        else
        {
            instance.GetType()
                .GetMethod(
                    RowDataConvertingMethodName,
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance)?
                .Invoke(instance, new object[] { rowData }); 
        }
        return instance;
    }
    public static object ToObject(this RowData rowData, string type)
    {
        Type instanceType = Type.GetType(type);
        if(instanceType is null) throw new Exception($"Type is null : {type}");
        object instance = Activator.CreateInstance(instanceType);
        Type excelWriterType = instance.GetType().GetInterface("IFromRowData");
        if(excelWriterType is null)
        {
            rowData.SetPropertiesValueIfExist(instance);
            rowData.SetFieldsValueIfExist(instance);
        }
        else
        {
            instance.GetType()
                .GetMethod(
                    RowDataConvertingMethodName,
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance)?
                .Invoke(instance, new object[] { rowData });
        }

        return instance; //JsonConvert.SerializeObject(instance);
    }
    private static object ArrayConvertStringToValue(string type, string value)
    {
        string[] values = value.Split(',');
        switch (type)
        {
            case "string[]":
                return values;
            case "int[]":
                int[] intValues = new int[values.Length];
                for (int index = 0; index < values.Length; index++)
                {
                    intValues[index] = int.TryParse( values[index], out int intValue) ? intValue : 0;
                }
                return intValues;
            case "float[]":
                float[] floatValues = new float[values.Length];
                for (int index = 0; index < values.Length; index++)
                {
                    floatValues[index] = float.TryParse( values[index], out float floatValue) ? floatValue : 0;
                }
                return floatValues;
            case "bool[]":
                bool[] boolValues = new bool[values.Length];
                for (int index = 0; index < values.Length; index++)
                {
                    boolValues[index] = bool.TryParse( values[index], out bool boolValue) && boolValue;
                }
                return boolValues;
        }

        return null;
    }
    
    private static object ConvertStringToValue(string type, string value)
    {
        switch (type)
        {
            case "string":
                return value;
            case "int":
                return int.TryParse( value, out int intValue) ? intValue : 0;
            case "float":
                return  float.TryParse( value, out float floatValue) ? floatValue : 0;
            case "bool":
                return bool.TryParse( value, out bool boolValue) && boolValue;
            case "string[]":
            case "int[]":
            case "float[]":
            case "bool[]":
                return ArrayConvertStringToValue(type, value);                 
        }

        if (!type.Contains("enum", StringComparison.OrdinalIgnoreCase)) return null;
        
        string[] split = type.Split(SplitSignature);
        if(split.Length != 2) throw  new Exception($"Enum Type is not valid : {type}");
        string enumTypeString = split[1];
        if(string.IsNullOrEmpty(enumTypeString)) throw  new Exception($"Enum Type is not valid : {type}");
        Type enumType = Type.GetType(enumTypeString);
        if(enumType is null) throw  new Exception($"Enum Type is not valid : {type}");
             
        return Enum.Parse(enumType, value);
    }
    
    
    private static void SetPropertiesValueIfExist<T>(this RowData rowData, T instance)
    {
        Type instanceType = typeof(T);
        for (var columnIndex = 0; columnIndex < rowData.Headers.Count; columnIndex++)
        {
            string header = rowData.Headers[columnIndex];
            string type = rowData.Types[columnIndex];
            string value = rowData.Values[columnIndex];
            try
            {
                instanceType.GetProperty(
                        header,
                        BindingFlags.Public |
                        BindingFlags.NonPublic |
                        BindingFlags.Instance)? 
                    .SetValue(instance, ConvertStringToValue(type, value));
            }
            catch (Exception e)
            {
                Debug.LogError($@"{rowData.ExceptionLogInfoToString(typeof(T).Name,
                    columnIndex, header, type, value, e)}");
            }
        }
    }
    
    private static void SetFieldsValueIfExist<T>(this RowData rowData, T instance)
    {
        Type instanceType = typeof(T);
        for (var columnIndex = 0; columnIndex < rowData.Headers.Count; columnIndex++)
        {
            string header = rowData.Headers[columnIndex];
            string type = rowData.Types[columnIndex];
            string value = rowData.Values[columnIndex];
            
            try
            {
                instanceType.GetField(
                        header,
                        BindingFlags.Public |
                        BindingFlags.NonPublic |
                        BindingFlags.Instance)?
                    .SetValue(instance, ConvertStringToValue(type, value));
            }
            catch (Exception e)
            {
                Debug.LogError($@"{rowData.ExceptionLogInfoToString(typeof(T).Name,
                    columnIndex, header, type, value, e)}");
            }
        }
    }

    private static void SetPropertiesValueIfExist(this RowData rowData, object instance)
    {
        Type instanceType = instance.GetType();
        for (var columnIndex = 0; columnIndex < rowData.Headers.Count; columnIndex++)
        {
            string header = rowData.Headers[columnIndex];
            string type = rowData.Types[columnIndex];
            string value = rowData.Values[columnIndex];
            try
            {
                instanceType.GetProperty(
                        header,
                        BindingFlags.Public |
                        BindingFlags.NonPublic |
                        BindingFlags.Instance)?
                    .SetValue(instance, ConvertStringToValue(type, value));
            }
            catch (Exception e)
            {
                Debug.LogError($@"{rowData.ExceptionLogInfoToString(instanceType.Name,
                    columnIndex, header, type, value, e)}");
            }
        }
    }

    private static void SetFieldsValueIfExist(this RowData rowData, object instance)
    {
        Type instanceType = instance.GetType();
        for (var columnIndex = 0; columnIndex < rowData.Headers.Count; columnIndex++)
        {
            string header = rowData.Headers[columnIndex];
            string type = rowData.Types[columnIndex];
            string value = rowData.Values[columnIndex];

            try
            {
                instanceType.GetField(
                        header,
                        BindingFlags.Public |
                        BindingFlags.NonPublic |
                        BindingFlags.Instance)?
                    .SetValue(instance, ConvertStringToValue(type, value));
            }
            catch (Exception e)
            {
                Debug.LogError($@"{rowData.ExceptionLogInfoToString(instanceType.Name,
                    columnIndex, header, type, value, e)}");
            }
        }
    }

    private static string ExceptionLogInfoToString(this RowData rowData,
        string typeName,
        int columnIndex,
        string header,
        string itemType,
        string itemValue,
        Exception e)
    {
        return $@"
[Info]
    - TopCategory           : {rowData.Values[ExcelHeaderInfo.TopCategoryIndex]}
    - MiddleCategory        : {rowData.Values[ExcelHeaderInfo.MiddleCategoryIndex]}
    - BottomCategory        : {rowData.Values[ExcelHeaderInfo.BottomCategoryIndex]}
    - ObjectName            : {rowData.Values[ExcelHeaderInfo.ObjectNameIndex]}
    - ObjectID              : {rowData.Values[ExcelHeaderInfo.InstanceIDIndex]}
    - DisplayName           : {rowData.Values[ExcelHeaderInfo.DisplayNameIndex]}

*   {typeName} | {columnIndex} 

    rowData.Headers         :   {rowData.Headers.Count} | {header}
    rowData.Types           :   {rowData.Types.Count} | {itemType}
    rowData.Values          :   {rowData.Values.Count} | {itemValue}

*   Message
    {e}
";
    }
    
    public static DataTable ToDataTable(this RowData rowData)
    {
        DataTable dt = new DataTable(rowData.MiddleCategory);

        // Add Base Column
        dt.Columns.Add("TopCategory", typeof(string)).DefaultValue = rowData.TopCategory;
        dt.Columns.Add("MiddleCategory", typeof(string)).DefaultValue = rowData.MiddleCategory;
        dt.Columns.Add("BottomCategory", typeof(string)).DefaultValue = rowData.BottomCategory;
        dt.Columns.Add("ObjectName", typeof(string)).DefaultValue = rowData.ObjectName;
        dt.Columns.Add("InstanceID", typeof(int)).DefaultValue = rowData.ObjectID;

        // Add The other Headers
        for (int index = 0; index < rowData.Headers.Count; index++)
        {
            dt.Columns.Add(rowData.Headers[index], typeof(string));
        }

        // Add Values 
        DataRow row = dt.NewRow();
        for (int index = 0; index < rowData.Values.Count; index++)
        {
            row[rowData.Headers[index]] = rowData.Values[index];
        }
        dt.Rows.Add(row);

        return dt;
    }

    public static DataSet ToDataSet(this RowData rowData)
    {
        DataSet ds = new DataSet(rowData.TopCategory);
        ds.Tables.Add(rowData.ToDataTable());
        return ds;
    }
}