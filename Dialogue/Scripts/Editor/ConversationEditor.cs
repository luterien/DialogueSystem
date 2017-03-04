using UnityEngine;
using UnityEditor;

namespace DialogueSystem { 

    public class ConversationEditor : EditorWindow {

        GUIStyle nodeStyle;

        public Conversation conversation;

        private Rect informationText;

        private Vector2 drag;
        private bool dragging;

        [MenuItem("Window/Conversation Editor")]
        private static void OpenWindow() {
            ConversationEditor window = GetWindow<ConversationEditor>();
            window.titleContent = new GUIContent("Conversation Editor");
        }

        private void OnEnable() {

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
            nodeStyle.border = new RectOffset(12, 12, 12, 12);

        }

        private void OnGUI() {

            if (conversation == null) {
                DrawInfoBox("Click here to select a conversation to edit.");
                ProcessInfoBoxEvents(Event.current);
            } else {
                DrawInfoBox("Conversation : " + conversation.name);
                DrawConversation();
                DrawConnections();
                ProcessEvents(Event.current);
                ProcessNodeEvents(Event.current);
                ProcessInfoBoxEvents(Event.current);
            }

        }

        private void ProcessEvents(Event e) {

            drag = Vector2.zero;

            switch (e.type) {

                case EventType.MouseDown:
                    if (e.button == 1) {
                        foreach (Node node in conversation.nodes) {
                            if (node.rect.Contains(e.mousePosition)) {
                                ProcessNodeMenu(node);
                                return;
                            }
                        }
                        ProcessContextMenu(e.mousePosition);
                    }
                    if (e.button == 0) {
                        SelectNode();
                        if (!ClickingOnNodes(e.mousePosition)) {
                            dragging = true;
                        }
                    }
                    break;

                case EventType.MouseUp:
                    if (e.button == 0 && !ClickingOnNodes(e.mousePosition)) {
                        dragging = false;
                    }
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0 && dragging) {
                        OnDrag(e.delta);
                    }
                    break;

            }

        }

        private bool ClickingOnNodes(Vector2 mousePosition) {
            foreach (Node node in conversation.nodes) {
                if (node.rect.Contains(mousePosition)) {
                    return true;
                }
            }
            return false;
        }

        private void OnDrag(Vector2 delta) {
            drag = delta;

            if (conversation.nodes != null) {
                for (int i = 0; i < conversation.nodes.Count; i++) {
                    conversation.nodes[i].Drag(delta);
                }
            }

            GUI.changed = true;
        }

        private void ProcessInfoBoxEvents(Event e) {

            switch(e.type) {

                case EventType.MouseDown:
                    if (informationText.Contains(e.mousePosition)) {
                        int controlID = EditorGUIUtility.GetControlID(FocusType.Passive);
                        EditorGUIUtility.ShowObjectPicker<Conversation>(null, false, "", controlID);
                    }
                    break;

                case EventType.ExecuteCommand:
                    Object obj = EditorGUIUtility.GetObjectPickerObject();
                    if (obj != null && obj.GetType() == typeof(Conversation)) {
                        conversation = (Conversation)obj;
                        OnConversationPicked();
                    }
                    break;

            }

        }

        private void ProcessNodeEvents(Event e) {
            if (conversation != null) {
                for (int i = conversation.nodes.Count - 1; i >= 0; i--) {
                    bool guiChanged = conversation.nodes[i].ProcessEvents(e);
                    if (guiChanged) {
                        GUI.changed = true;
                    }
                }
            }
        }

        private void DrawConversation() {
            foreach (Node node in conversation.nodes) {
                node.Draw();
            }
        }

        private void DrawInfoBox(string text) {
            informationText = new Rect(0f, 0f, position.width * 0.5f, 20f);
            GUI.Box(informationText, text);
        }

        private void ProcessContextMenu(Vector2 mousePosition) {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Add node"), false, () => AddNode(mousePosition));
            genericMenu.ShowAsContext();
        }

        private void ProcessNodeMenu(Node node) {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Connect"), false, () => ConnectionHandler.Get().Add(node));
            genericMenu.ShowAsContext();
        }

        private void AddNode(Vector2 mousePosition) {
            if (conversation != null) {
                conversation.AddNode(
                    new Node(mousePosition, "Random Content")
                );
                OnUpdate();
            }
        }

        private void DrawConnections() {
            for (int i=0; i < conversation.nodes.Count; i++) {
                for (int j=0; j < conversation.nodes[i].children.Count; j++) {
                    Handles.DrawBezier(
                        conversation.nodes[i].rect.center,
                        conversation.nodes[i].children[j].rect.center,
                        conversation.nodes[i].rect.center + Vector2.left * 50f,
                        conversation.nodes[i].children[j].rect.center - Vector2.left * 50f,
                        Color.white,
                        null,
                        2f
                    );
                }
            }
        }

        private void OnUpdate() {
            if (conversation) EditorUtility.SetDirty(conversation);
            Debug.Log("saving to disk");
        }

        private void OnConversationPicked() {
            conversation.Prepare();
        }

        private void SelectNode() {
            Selection.activeObject = conversation;
        }

    }

}