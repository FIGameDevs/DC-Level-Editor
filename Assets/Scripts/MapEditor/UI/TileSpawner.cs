using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSpawner : MonoBehaviour
{
    [SerializeField]
    Button normalB;
    [SerializeField]
    Button innerB;
    [SerializeField]
    Button outerB;

    GameObject[] variations;
    GameObject[] innerCorners;
    GameObject[] outerCorners;

    public void SetTile(TileType tile)
    {
        variations = tile.variations;
        innerCorners = tile.innerCorners;
        outerCorners = tile.outerCorners;

        if (tile.variations.Length == 0)
        {
            normalB.interactable = false;
        }
        if (tile.innerCorners.Length == 0)
        {
            innerB.interactable = false;

        }
        if (tile.outerCorners.Length == 0)
        {
            outerB.interactable = false;
        }
    }

    public void SetTile(GameObject[] tile)
    {
        variations = tile;
        innerB.interactable = false;
        outerB.interactable = false;
    }

    public void SpawnNormal()
    {
        if (variations != null && variations.Length > 0)
        {
            var go = Instantiate(variations[0]);
            var pos = GridPlaneControl.LookIntersect();
            pos = new Vector3(Round.ToFour(pos.x), Round.ToThreePointSix(pos.y), Round.ToFour(pos.z));
            go.transform.position = pos;
            BlockManipulator.SelectItem(go);
        }
    }

    public void SpawnInner()
    {
        if (innerCorners != null && innerCorners.Length > 0)
        {
            var go = Instantiate(innerCorners[0]);
            var pos = GridPlaneControl.LookIntersect();
            pos = new Vector3(Round.ToFour(pos.x), Round.ToThreePointSix(pos.y), Round.ToFour(pos.z));
            go.transform.position = pos;
            BlockManipulator.SelectItem(go);

        }
    }

    public void SpawnOuter()
    {
        if (outerCorners != null && outerCorners.Length > 0)
        {
            var go = Instantiate(outerCorners[0]);
            var pos = GridPlaneControl.LookIntersect();
            pos = new Vector3(Round.ToFour(pos.x), Round.ToThreePointSix(pos.y), Round.ToFour(pos.z));
            go.transform.position = pos;
            BlockManipulator.SelectItem(go);

        }
    }
}
