using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BackgroundLine))]
public class BackgroundLineEditor : Editor {
    BackgroundLine myScript;
    bool editMode;
	void OnEnable()
    {
        myScript = (BackgroundLine)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate!"))
        {
            if (myScript.linePrefab == null)
                Debug.LogError("Line Prefab을 넣어주세요.");
            else
                myScript.LineGenerate();
        }

        if(editMode)
        {
            GUI.color = Color.red;
            myScript.LineGenerate();
        }
        if (GUILayout.Button("EditMode"))
        {
            if (editMode && myScript.linePrefab == null)
            {
                Debug.LogError("Line Prefab을 넣어주세요.");
                editMode = false;
            }
            else
                editMode = !editMode;
        }
        GUI.color = Color.white;
    }
}
