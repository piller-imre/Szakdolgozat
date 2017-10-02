using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MapGenerator))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapGenerator myScript = (MapGenerator)target;

        if (GUILayout.Button("Create Map"))
        {
            myScript.ClearMap();
            myScript.CreateMap();
        }

        if (GUILayout.Button("Clear Map"))
        {
            myScript.ClearMap();
        }
    }

}
