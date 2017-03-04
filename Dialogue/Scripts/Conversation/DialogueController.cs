using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {
    
    static public DialogueController get;

    public GameObject dialogueUI;
    public GameObject contentUI;
    public Button optionPrefab;
    public GameObject optionsHolder;

    void Awake() {
        if (get == null) {
            get = this;
        } else if (get != this) {
            Destroy(get);
        }
    }

    public void StartConversation(Node node) {
        dialogueUI.SetActive(true);
        UpdateNode(node);
    }

    public void EndConversation() {
        dialogueUI.SetActive(false);
        contentUI.GetComponent<Text>().text = null;
        RemoveOptions();
    }

    private void UpdateNode(Node node) {
        contentUI.GetComponent<Text>().text = node.text;
        RemoveOptions();
        AddOptions(node);
    }

    private void RemoveOptions() {
        foreach (Transform child in optionsHolder.transform) {
            Destroy(child.gameObject);
        }
    }

    private void AddOptions(Node node) {
        foreach (Node childNode in node.children) {
            Button optionButton = Instantiate(optionPrefab, optionsHolder.transform);
            optionButton.transform.FindChild("Text").GetComponent<Text>().text = childNode.text;
            optionButton.onClick.AddListener(() => UpdateNode(childNode));
        }
    }
    
}
