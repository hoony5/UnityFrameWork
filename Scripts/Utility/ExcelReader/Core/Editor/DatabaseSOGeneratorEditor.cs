using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Utility.ExcelReader
{
    public class DatabaseSOGeneratorEditor : EditorWindow
    {
        private DatabaseSOGenerator databaseSOGenerator = new DatabaseSOGenerator();

        [MenuItem("DDD/Database SO Generator")]
        public static void ShowWindow()
        {
            GetWindow<DatabaseSOGeneratorEditor>("DatabaseSOGenerator");
        }

        private void OnGUI()
        {
#if UNITY_EDITOR
            GUILayout.Label("Database Path", EditorStyles.boldLabel);
            GUILayout.Space(10);
            databaseSOGenerator.databasePath =
                EditorGUILayout.TextField("Database Path", databaseSOGenerator.databasePath);
            GUILayout.Space(5);
            databaseSOGenerator.destinationPath =
                EditorGUILayout.TextField("Destination Path", databaseSOGenerator.destinationPath);
            GUILayout.Space(10);
            if (GUILayout.Button("Generate ExternalInit"))
            {
                databaseSOGenerator.GenerateExternalInit();
            }
            if (GUILayout.Button("Generate Model Scripts"))
            {
                databaseSOGenerator.GenerateModel();
            }
            if (GUILayout.Button("Generate DatabaseSO"))
            {
                databaseSOGenerator.GenerateDatabaseSO();
            }

            GUILayout.Space(10);
            if (GUILayout.Button("Remove"))
            {
                databaseSOGenerator.Remove();
            }
#endif
        }
    }
}
