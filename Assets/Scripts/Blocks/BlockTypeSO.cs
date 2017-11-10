using System.Collections;
using System.Collections.Generic;
using System;   
using UnityEngine;

[CreateAssetMenu(fileName = "New Block Type", menuName = "Block Type")]
[Serializable]
public class BlockTypeSO : ScriptableObject
{
    public string blockName = "New block type";
    public TileType[] wallTiles;
    public TileType[] floorTiles;
    public TileType[] ceilingTiles;

    public GameObject[] traps;
    public GameObject[] puzzles;
    public GameObject[] other;

    public BlockTypeSO[] similarBlocks;
}


[Serializable]
public class TileType
{
    public GameObject[] variations;
    public GameObject[] innerCorners;
    public GameObject[] outerCorners;

}