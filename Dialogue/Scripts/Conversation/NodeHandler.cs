using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHandler {

    private static readonly NodeHandler instance = new NodeHandler();
    
    private NodeHandler() { }

    public static NodeHandler Get() {
        return instance;
    }

    public void Add(Node node) {
        
    }

}
