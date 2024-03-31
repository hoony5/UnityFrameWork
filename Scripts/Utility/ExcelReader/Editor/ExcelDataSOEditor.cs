using UnityEditor;
using UnityEngine;
using Utility.ExcelReader;

[CustomEditor(typeof(ExcelDataSO))]
public class ExcelDataSOEditor : Editor
{
    ExcelDataSO _excelDataSO;
    private string _excelDataSOKey;
    private void OnEnable()
    {
        _excelDataSO = target as ExcelDataSO;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.Space(30);
        EditorGUILayout.HelpBox($"Automatically, the excel is loaded when placing it on the Object PropertyField. if you want to load it manually, press the button below.", MessageType.Info, true);
        EditorGUILayout.Space(30);
        if (GUILayout.Button("Load Excel Data", GUILayout.Height(30)))
        {
            _excelDataSO.OnValidate();
        }
        EditorGUILayout.Space(10);
        if (GUILayout.Button("Clear Excel Data", GUILayout.Height(30)))
        {
            _excelDataSO.Clear();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
