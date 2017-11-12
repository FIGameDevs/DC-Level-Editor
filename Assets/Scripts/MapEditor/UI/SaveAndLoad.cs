using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{

    [SerializeField]
    UnityEngine.UI.InputField input;

    static UnityEngine.UI.InputField inputInstance;
    private void Start()
    {
        inputInstance = input;
    }

    public static UnityEngine.UI.InputField GetInputField()
    {
        return inputInstance;
    }

    public void Save()
    {
        if (input.text == "")
            return;

        MapSerializer.SaveMap(Application.streamingAssetsPath + "/maps/" + input.text + ".map");
    }

    public void Load()
    {
        if (input.text == "")
            return;

        MapSerializer.LoadMap(Application.streamingAssetsPath + "/maps/" + input.text + ".map");
    }
}
