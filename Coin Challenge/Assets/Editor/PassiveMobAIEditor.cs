using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PassiveMobAI))]
public class PassiveMobAIEditor : Editor
{   
    private void OnSceneGUI()
    {
        PassiveMobAI _deer = (PassiveMobAI)target;
        
        Color c = Color.green;
        
        if(_deer.alertStage == AlertStage.Alerted)
            c = Color.red;

        Handles.color = new Color(c.r, c.g, c.b, 0.3f);
        Handles.DrawSolidArc(
        _deer.transform.position,
        _deer.transform.up, 
        Quaternion.AngleAxis(-_deer.fovAngle / 2f, _deer.transform.up) * _deer.transform.forward, 
        _deer.fovAngle, _deer.fov);

        Handles.color = c;
        _deer.fov = Handles.ScaleValueHandle(_deer.fov, _deer.transform.position + _deer.transform.forward * _deer.fov, _deer.transform.rotation, 3, Handles.SphereHandleCap, 1);
    }
    
}
