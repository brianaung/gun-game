using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapBuilder))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MapBuilder dungeonCreator = (MapBuilder)target;
        if (GUILayout.Button("Create New Map"))
        {
            dungeonCreator.CreateDungeon();
        }
    }
}
