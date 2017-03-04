using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class ConnectionPoint {

    public enum ConnectionType {
        In, Out
    }
    public ConnectionType connectionType;

    public Rect rect;
    public GUIStyle style;

    public ConnectionPoint(Rect nodeRect) {
        rect = new Rect(nodeRect.position.x - 5f, nodeRect.position.y +5f, 5f, 5f);
    }

    public void Enable() {
        if (style == null) {
            style = new GUIStyle();
            style.normal.background = EditorGUIUtility.Load("Assets/green_button02.png") as Texture2D;
            style.alignment = TextAnchor.MiddleCenter;
        }
    }
    
    public void Draw() {
        GUI.Box(rect, "", style);
    }

    public void Drag() {

    }

}
