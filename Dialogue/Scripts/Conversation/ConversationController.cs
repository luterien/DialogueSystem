using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationController : MonoBehaviour {

    public Conversation conversation;

    void OnTriggerEnter(Collider collider) {
        DialogueController.get.StartConversation(conversation.nodes[0]);
    }

    void OnTriggerExit(Collider collider) {
        DialogueController.get.EndConversation();
    }

}
