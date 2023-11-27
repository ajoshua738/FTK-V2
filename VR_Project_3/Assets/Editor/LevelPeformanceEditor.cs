using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PerformanceTracker))]
public class LevelPeformanceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PerformanceTracker levelPerformance = (PerformanceTracker)target;

        GUILayout.Space(10);

        GUILayout.Label("Select File:");

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Browse"))
        {
            string path = EditorUtility.OpenFilePanel("Select File", "", ""); // Opens a file picker dialog

            if (!string.IsNullOrEmpty(path))
            {
                string fileName = Path.GetFileName(path); // Get only the file name from the path
                levelPerformance.fileName = fileName; // Set the selected file name to the public string filename in your script
                EditorUtility.SetDirty(levelPerformance); // Mark the script as dirty to save changes
            }
        }

        GUILayout.EndHorizontal();
    }
}
