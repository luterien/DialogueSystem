using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionHandler {

    private static readonly ConnectionHandler instance = new ConnectionHandler();

    public Node parent;
    public Node child;

    private ConnectionHandler() { }

    public static ConnectionHandler Get() {
        return instance;
    }

    public void Add(Node node) {
        if (parent == null) {
            parent = node;
        } else {
            child = node;
            FormConnection();
            Reset();
        }
    }

    private void FormConnection() {
        parent.AddChild(child);
    }

    private void Reset() {
        parent = null;
        child = null;
    }

}
