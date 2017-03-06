using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class Node {

    [HideInInspector]
    public Rect rect;
    
    [HideInInspector]
    public string id;

    GUIStyle nodeStyle;

    //public ConnectionPoint inPoint;
    //public ConnectionPoint outPoint;

    float height = 100f;
    float width = 200f;

    [HideInInspector]
    public bool isBeingDragged = false;

    public bool initialized = false;

    [TextArea(3, 10)]
    public string text;

    [HideInInspector]
    public List<Node> children = new List<Node>();
    [HideInInspector]
    public List<string> childrenIds = new List<string>();

    public Node(Vector2 position, string content) {

        rect = new Rect(position.x, position.y, width, height);

        text = content;

        Enable();

    }

    public void Enable() {
        
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        nodeStyle.alignment = TextAnchor.MiddleCenter;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);

        if (id == null) {
            id = Guid.NewGuid().ToString();
        }
        
        //inPoint = new ConnectionPoint(rect);
        //inPoint.Enable();

        initialized = true;

    }

    public void Draw() {
        GUI.Box(rect, text, nodeStyle);
        //inPoint.Draw();
    }

    public void Drag(Vector2 delta) {
        rect.position += delta;
    }

    public bool ProcessEvents(Event e) {

        switch (e.type) {

            case EventType.MouseDown:
                if (e.button == 0) {
                    if (rect.Contains(e.mousePosition)) {
                        isBeingDragged = true;
                        GUI.changed = true;
                    } else {
                        GUI.changed = true;
                    }
                }
                break;

            case EventType.MouseUp:
                if (e.button == 0) {
                    isBeingDragged = false;
                }
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && isBeingDragged) {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;

        }

        return false;

    }

    public void AddChild(Node node) {
        children.Add(node);
        childrenIds.Add(node.id);
    }

    public void RemoveChild(Node node) {
        children.Remove(node);
        childrenIds.Remove(node.id);
    }

    public void OnUpdate() {

    }

}
