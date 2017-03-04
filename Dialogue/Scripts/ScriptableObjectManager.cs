using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectManager {

    public string AssetPath = "Assets/Dialogue/Data/Conversations";

    public void CreateAsset<T>() where T : ScriptableObject {

        T asset = ScriptableObject.CreateInstance<T>();



    }

}
