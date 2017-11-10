using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListBlockTypes : MonoBehaviour {
    [SerializeField]
    AllBlocksSO allBlocks;
    [SerializeField]
    GameObject textPrefab;
    [SerializeField]
    GameObject content;

    public static AllBlocksSO AllBlocks { get; private set;}

    private void Start()
    {
        AllBlocks = allBlocks;
        for (int i = 0; i < allBlocks.types.Length; i++)
        {
            var go = Instantiate(textPrefab);
            go.transform.SetParent(content.transform);
            go.GetComponent<Text>().text = allBlocks.types[i].blockName;
            go.GetComponent<ChooseBType>().Id = i;
        }
    }
}
