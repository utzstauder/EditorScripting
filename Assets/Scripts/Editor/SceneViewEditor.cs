using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class SceneViewEditor : Editor
{
    public static bool EnableEditor
    {
        get{ return EditorPrefs.GetBool("EnableEditor", false); }
        set{ EditorPrefs.SetBool("EnableEditor", value); }
    }

    static Vector3 handlePosition;




    static SceneViewEditor()
    {
        SceneView.onSceneGUIDelegate += OnSceneGUI;
        // SceneView.beforeSceneGui or SceneView.duringSceneGui in newer version!
    }

    static void OnSceneGUI(SceneView sceneView)
    {
        if (EditorApplication.isPlaying
        || EditorApplication.isPlayingOrWillChangePlaymode
        || EditorApplication.isPaused
        || EditorApplication.isCompiling
        || EditorApplication.isUpdating) return;

        if (!EnableEditor) return;

        // override default control
        int controlId = GUIUtility.GetControlID(FocusType.Passive);
        HandleUtility.AddDefaultControl(controlId);

        UpdateHandlePosition();
        
        if (Event.current.type == EventType.MouseDown
        && Event.current.button == 0)
        {
            // ...
            Debug.Log("Left mouse button down");
        }

        DrawHandle(handlePosition);

        SceneView.RepaintAll();
    }

    static void UpdateHandlePosition()
    {
        Vector2 mousePosition = new Vector2(
            Event.current.mousePosition.x,
            Event.current.mousePosition.y
        );

        Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            handlePosition = SnapToGrid(hit.point);

            // Debug.Log(hit.point);
        }

        //Debug.Log(mousePosition);
    }

    static void DrawHandle(Vector3 currentHandlePosition)
    {
        Handles.color = Color.red;
        Handles.DrawWireCube(currentHandlePosition, Vector3.one);
    }

    static Vector3 SnapToGrid(Vector3 position)
    {
        return new Vector3(
            Mathf.Round(position.x),
            Mathf.Round(position.y),
            Mathf.Round(position.z)
        );
    }

    [MenuItem("Tools/Toggle Editor")]
    public static void ToggleEditorState()
    {
        EnableEditor = !EnableEditor;
    }
}
