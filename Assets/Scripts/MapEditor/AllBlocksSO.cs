using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "All Blocks", menuName = "All Blocks")]
public class AllBlocksSO : ScriptableObject {
    [SerializeField]
    public BlockTypeSO[] types;

    private void OnEnable()
    {
        var guids = UnityEditor.AssetDatabase.FindAssets("t:BlockTypeSO");
        var items = new string[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            items[i] = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]);
        }
        types = new BlockTypeSO[items.Length];
        for (int i = 0; i < types.Length; i++)
        {
            types[i] = UnityEditor.AssetDatabase.LoadAssetAtPath<BlockTypeSO>(items[i]);
        }
    }
}
