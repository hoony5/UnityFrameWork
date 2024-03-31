using UnityEditor;
using UnityEngine;

namespace Diagram.Config
{
    [CustomEditor(typeof(DiagramConfig))]
    public class DiagramConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GUILayout.Space(30);
            if (GUILayout.Button("Open Slack Webhook configuration"))
            {
                Application.OpenURL("https://my.slack.com/services/new/incoming-webhook/");
            }
            if (GUILayout.Button("Open Confluence"))
            {
                Application.OpenURL("https://www.atlassian.com/software/confluence");
            }
            if (GUILayout.Button("Open Document for more information"))
            {
            }
        }
    }
}