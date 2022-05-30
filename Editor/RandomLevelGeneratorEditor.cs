using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// created with the help of a video  : https://www.youtube.com/watch?v=RInUu1_8aGw

// essentially this creates a custom button in th editor to generate a new level when pressed;

[CustomEditor(typeof(AbstractLevelGenerator), true)]
public class RandomLevelGeneratorEditor : Editor
{ 
    AbstractLevelGenerator generator;

    private void Awake()
    {
        generator = (AbstractLevelGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }
    }
}
