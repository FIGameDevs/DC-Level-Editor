using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseBType : MonoBehaviour, IPointerClickHandler
{

    public int Id = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        ListTileTypes.Instance.ShowTypesOfTile(ListBlockTypes.AllBlocks.types[Id], new BlockInfo() {BlockId = Id,UpperVariation = 0, Variation = 0, MyTileKind = TileKind.Normal, TileTypeId = 0 });
    }
}
