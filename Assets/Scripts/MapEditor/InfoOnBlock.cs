using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileKind
{
    Normal, InnerCorner, OuterCorner
}

public struct BlockInfo // must be struct
{
    public int BlockId;
    public int TileTypeId;//walls, floors, traps...
    public int UpperVariation;
    public TileKind MyTileKind;
    public int Variation;
}

public class InfoOnBlock : MonoBehaviour
{

    public BlockInfo Info;


}
