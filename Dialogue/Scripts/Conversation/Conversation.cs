using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "DialogueSystem/Conversation")]
public class Conversation : ScriptableObject {

    public List<Node> nodes = new List<Node>();

    public void AddNode(Node node) {
        nodes.Add(node);
    }

    public void RemoveNode(Node node) {
        // remove as a child
        for (int i=0; i < nodes.Count; i++) {
            if (nodes[i].children.Contains(node)) {
                nodes[i].RemoveChild(node);
            }
        }
        // remove from the list
        nodes.Remove(node);
    }

    public void Prepare() {
        // set children
        for (int i=0; i < nodes.Count; i++) {
            nodes[i].children = new List<Node>();
            for (int j=0; j < nodes[i].childrenIds.Count; j++) {
                Node child = nodes.Find(n => n.id == nodes[i].childrenIds[j]);
                if (child != null) nodes[i].children.Add(child);
            }
        }
        // enable nodes
        foreach (Node node in nodes) node.Enable();
    }

}
