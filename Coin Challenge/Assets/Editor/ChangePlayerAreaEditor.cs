using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChangePlayerArea))]
public class ChangePlayerAreaEditor : Editor
{
    ChangePlayerArea _target;

    public enum AreaName
    {
        huntPos, testPos, labyPos
    }
    
    public AreaName areaName;
    
    void OnEnable()
    {
        _target = (ChangePlayerArea) target;
        SearchPlayer();
    }



    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        areaName = (AreaName) EditorGUILayout.EnumPopup("area", areaName);
        if(GUILayout.Button("Move to " + areaName.ToString()))
        {
            MovePlayer(areaName);
        }
    }


    void SearchPlayer()
    {
        if(_target.player != null)
        return;

        _target.player = GameObject.Find("Player").transform;
    }

    public void MovePlayer(AreaName area)
    {
       _target.player.position = GetPosByName(area);
    }

    public Vector3 GetPosByName(AreaName area)
    {
        switch(area)
        {
            case AreaName.huntPos:
            return _target.huntPos.position;
            default: 
            return Vector3.zero;
            
        }
    }
}
