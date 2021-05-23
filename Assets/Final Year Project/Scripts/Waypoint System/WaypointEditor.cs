using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    Waypoint Waypoint => target as Waypoint;

    private void OnSceneGUI()
    {
        Handles.color = Color.red;
        for (int i = 0; i < Waypoint.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            // Create Handles
            Vector3 currentWaypointPoint = Waypoint.CurrentPosition + Waypoint.Points[i];
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint,
                Quaternion.identity, 0.7f,
                new Vector3(x: 0.3f, y: 0.3f, z: 0.3f), Handles.SphereHandleCap);

            // Create text (numbers)
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold; //bold font
            textStyle.fontSize = 16; // font size
            textStyle.normal.textColor = Color.black; // green text
            Vector3 textAllignment = Vector3.down * 0.35f + Vector3.right * 0.35f; // allignment is at the bottom right
            Handles.Label(Waypoint.CurrentPosition + Waypoint.Points[i] + textAllignment,
                $"{i + 1}", textStyle); // show index from 1
            //EditorGUI.EndChangeCheck();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Move Handle"); // free move handle
                Waypoint.Points[i] = newWaypointPoint - Waypoint.CurrentPosition; //update position
            }
        }
    }
}

