using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Rotator))]
public class RotatorEditor : Editor
{
    void OnSceneGUI()
    {
        // Debug.Log("OnSceneGUI()");

        Rotator rotator = target as Rotator;

        Vector3 fromVector = Quaternion.AngleAxis(
            angle: - rotator.angle / 2f,
            axis: rotator.transform.up
        ) * rotator.transform.forward;

        // Handles.Label(rotator.transform.position, "Label");
        Handles.color = new Color(0, 1f, 0, 0.1f);
        Handles.DrawSolidArc(
            center: rotator.transform.position,
            normal: rotator.transform.up,
            from: fromVector,
            angle: rotator.angle,
            radius: rotator.radius
        );

        Handles.BeginGUI();
        {
            if (GUI.Button(new Rect(20, 20, 50, 20), "Button")){
                Debug.Log("Click!");
            }
        }
        Handles.EndGUI();
    }
}
