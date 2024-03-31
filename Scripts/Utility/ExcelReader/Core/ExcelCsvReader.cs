using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using AYellowpaper.SerializedCollections;
using ExcelDataReader;
using Share;

namespace Utility.ExcelReader
{
    public static class ExcelCsvReader
    {
        private const string KorEnCoding = "ks_c_5601-1987";
        // key is middle category
        public static Dictionary<string, List<string>> ReadCSV(string path)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    string[] values = line.Split(',');
                    if (values.Length <= 0) continue;
                    if (result.ContainsKey(values[0]))
                    {
                        result[values[0]].AddRange(values);
                    }
                    else
                    {
                        result.Add(values[0], new List<string>(values));
                    }
                }
            }
            return result;
        }
        public static (string topCategory, SerializedDictionary<string, ExcelSheetInfo> map) Read(string filePath)
        {
            const int typeIndex = 0;
            const int topCategoryIndex = 0;
            string topCategory = string.Empty;

#if UNITY_EDITOR
            ExcelReaderConfiguration config = new ExcelReaderConfiguration
            {
                FallbackEncoding = Encoding.GetEncoding(KorEnCoding)
            };
            if(string.IsNullOrEmpty(filePath)) return default;
            using (FileStream streamer = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(streamer, config))
                {
                    DataSet dataset = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });
            
                    List<ColumnData> columnDataList = new List<ColumnData>();
                    SerializedDictionary<string, RowData> rowDataDict = new SerializedDictionary<string, RowData>(dataset.Tables.Count);
                    SerializedDictionary<string, ExcelSheetInfo> result = new SerializedDictionary<string, ExcelSheetInfo>(dataset.Tables.Count); 

                    foreach (DataTable table in dataset.Tables)
                    {
                        columnDataList.Clear();
                        rowDataDict.Clear();
                        
                        if(table.TableName.Contains("Enum")) continue;
                        ProcessColumns(typeIndex, table, columnDataList);
                        ProcessRows(typeIndex, table, rowDataDict);

                        ExcelSheetInfo excelSheetInfo = new ExcelSheetInfo
                        {
                            MiddleCategory = table.TableName
                        };
                        
                        if(columnDataList.Count > 0 && columnDataList[topCategoryIndex].Values.Count > 0)
                            topCategory = columnDataList[topCategoryIndex].Values[topCategoryIndex];
                        
                        foreach (ColumnData columnData in columnDataList)
                        {
                            if(columnData.Header.Contains(SharedValue.ExcelReader.BreakingSignature_Sharp)) break;

                            if(columnData.Header.Contains(SharedValue.ExcelReader.IgnoreSignature_Prefix) ||
                               columnData.Header.Contains(SharedValue.ExcelReader.IgnoreSignature_Slash) ||
                               columnData.Header.Contains(SharedValue.ExcelReader.IgnoreSignature_Prefix)) continue;
                            excelSheetInfo.ColumnDataDict.TryAdd(columnData.Header, columnData);

                        }
                        foreach (RowData rowData in rowDataDict.Values)
                        {
                            if(rowData.TopCategory.Contains(SharedValue.ExcelReader.BreakingSignature_Sharp)) break;
                            
                            if(rowData.TopCategory.Contains(SharedValue.ExcelReader.IgnoreSignature_Prefix) ||
                               rowData.TopCategory.Contains(SharedValue.ExcelReader.IgnoreSignature_Slash) ||
                               rowData.TopCategory.Contains(SharedValue.ExcelReader.IgnoreSignature_Prefix)) continue;
                            excelSheetInfo.RowDataDict.TryAdd(rowData.ObjectName, rowData);
                        }

                        _ = result.TryAdd(table.TableName, excelSheetInfo);
                    }

                    return (topCategory, result);
                }    
            }
#else
            return null;
#endif
        }

        private static void ProcessColumns(int typeIndex, DataTable table, List<ColumnData> columnDataList)
        {
            if (table.Columns.Count == 0) return;
            for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
            {
                DataColumn column = table.Columns[columnIndex];
                ColumnData columnData = new ColumnData
                {
                    Header = column.ColumnName,
                    Type = table.Rows[typeIndex][column].ToString(),
                    TableName = table.TableName
                };

                if (column.ColumnName.Contains($"{SharedValue.ExcelReader.IgnoreSignature_Prefix}{columnIndex}") ||
                    column.ColumnName.Contains(SharedValue.ExcelReader.IgnoreSignature_Slash) ||
                    column.ColumnName.Contains(SharedValue.ExcelReader.IgnoreSignature_UnderBar)) continue;

                columnData.Values = new List<string>();
                for (var rowIndex = typeIndex + 1; rowIndex < table.Rows.Count; rowIndex++)
                {
                    string value = table.Rows[rowIndex][column].ToString();
                    if (value.Contains(SharedValue.ExcelReader.BreakingSignature_Sharp)) break;

                    if(column.ColumnName.Contains("TopCategory") && string.IsNullOrEmpty(value) ||
                    value.Contains(SharedValue.ExcelReader.IgnoreSignature_Slash)) continue;
                    
                    value = NormalizeValue(value);

                    columnData.Values.Add(string.IsNullOrEmpty(value) ? string.Empty : value);
                }

                columnDataList.Add(columnData);
            }
        }

        private static void ProcessRows(int typeIndex, DataTable table, Dictionary<string, RowData> rowDataDict)
        {
            // rowLength =  header + type + value = 3  
            if (table.Rows.Count <= 3) return;
            for (int rowIndex = typeIndex + 1; rowIndex < table.Rows.Count; rowIndex++)
            {
                object[] row = table.Rows[rowIndex].ItemArray;
                RowData rowData = new RowData
                (
                    topCategory: table.Rows[rowIndex].ItemArray[ExcelHeaderInfo.TopCategoryIndex].ToString(),
                    middleCategory : table.Rows[rowIndex].ItemArray[ExcelHeaderInfo.MiddleCategoryIndex].ToString(),
                    bottomCategory : table.Rows[rowIndex].ItemArray[ExcelHeaderInfo.BottomCategoryIndex].ToString(),
                    objectName : table.Rows[rowIndex].ItemArray[ExcelHeaderInfo.ObjectNameIndex].ToString(),
                    objectID : int.TryParse(table.Rows[rowIndex].ItemArray[ExcelHeaderInfo.InstanceIDIndex].ToString(), out int instanceID) ? instanceID : -1,
                    columnCount : table.Columns.Count
                );

                if (string.IsNullOrEmpty(rowData.ObjectName)) continue;

                for (var columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
                {
                    string value = row[columnIndex].ToString();
                    string header = table.Columns[columnIndex].ColumnName;
                    
                    if (value.Contains(SharedValue.ExcelReader.BreakingSignature_Sharp)) break;
                    if (rowIndex is 1 && header.Contains("TopCategory") && string.IsNullOrEmpty(value)) continue;
                    if (header.Contains($"{SharedValue.ExcelReader.IgnoreSignature_Prefix}{columnIndex}") ||
                        header.Contains(SharedValue.ExcelReader.IgnoreSignature_Slash) ||
                        header.Contains(SharedValue.ExcelReader.IgnoreSignature_UnderBar)) continue;

                    rowData.Headers.Add(header);
                    rowData.Types.Add(table.Rows[typeIndex].ItemArray[columnIndex].ToString());
                    
                    value = NormalizeValue(value);

                    rowData.Values.Add(string.IsNullOrEmpty(value) ? string.Empty : value);
                }

                rowDataDict.TryAdd(rowData.ObjectName, rowData);
            }
        }

        private static string NormalizeValue(string value)
        {
            if (value.Equals(SharedValue.ExcelReader.Bool_TRUE, StringComparison.OrdinalIgnoreCase))
                value = "true";
            if (value.Equals(SharedValue.ExcelReader.Bool_FALSE, StringComparison.OrdinalIgnoreCase))
                value = "false";

            return value;
        }
    }
}
