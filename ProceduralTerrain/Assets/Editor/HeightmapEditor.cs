using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace HeightmapTerrain
{
    [CustomEditor(typeof(HeightmapGrid))]
    public class HeightmapEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            HeightmapGrid grid = (HeightmapGrid)target;

            if (GUILayout.Button("Generate Mesh"))
            {
                grid.UpdateChunks();
            }
        }
    }
}
