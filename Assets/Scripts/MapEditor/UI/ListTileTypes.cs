using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListTileTypes : MonoBehaviour
{

    [SerializeField]
    GameObject showPrefab;
    [SerializeField]
    Text variationText;

    public static ListTileTypes Instance { get; private set; }

    List<GameObject> spawnedMenus = new List<GameObject>();
    BlockInfo currentInfo;

    static string[] menuNames = new string[] { "Wall tiles", "Floor tiles", "Ceiling tiles", "Traps", "Puzzles", "Other" };


    private void Start()
    {
        Instance = this;
    }

    public void UpVariation() {
        if (currentInfo.UpperVariation > 49)
            return;
        variationText.text = (currentInfo.UpperVariation + 1).ToString();
        currentInfo.UpperVariation += 1;
        ShowTypesOfTile(ListBlockTypes.AllBlocks.types[currentInfo.BlockId], currentInfo);
    }

    public void DownVariation()
    {
        if (currentInfo.UpperVariation < 1)
            return;
        variationText.text = (currentInfo.UpperVariation - 1).ToString();
        currentInfo.UpperVariation -= 1;
        ShowTypesOfTile(ListBlockTypes.AllBlocks.types[currentInfo.BlockId], currentInfo);
    }

    public void ShowTypesOfTile(BlockTypeSO block, BlockInfo info)//There are two types of variations, this one for blocks that don't go together and other one which is just details
    {
        var variation = info.UpperVariation;
        currentInfo = info; 
        DestroyMenus();
        if (variation < block.wallTiles.Length)
            AddMenu(block.wallTiles[variation], 0);
        if (variation < block.floorTiles.Length)
            AddMenu(block.floorTiles[variation], 1);
        if (variation < block.ceilingTiles.Length)
            AddMenu(block.ceilingTiles[variation], 2);
        if (variation < block.traps.Length)
            AddMenu(block.traps, 3);
        if (variation < block.puzzles.Length)
            AddMenu(block.puzzles, 4);
        if (variation < block.other.Length)
            AddMenu(block.other, 5);

        variationText.text = currentInfo.UpperVariation.ToString();
    }

    void DestroyMenus()
    {
        for (int i = 0; i < spawnedMenus.Count; i++)
        {
            Destroy(spawnedMenus[i]);
        }
        spawnedMenus.Clear();
    }

    void AddMenu(TileType tile, int nameId)
    {
        var go = Instantiate(showPrefab);
        go.transform.SetParent(transform);
        go.transform.GetChild(0).GetComponent<Text>().text = menuNames[nameId];

        //set buttons inside
        var newInfo = currentInfo;
        newInfo.TileTypeId = nameId;
        go.GetComponent<TileSpawner>().SetTile(tile, newInfo);

        spawnedMenus.Add(go);
    }

    void AddMenu(GameObject[] tile, int nameId)
    {
        var go = Instantiate(showPrefab);
        go.transform.SetParent(transform);
        go.transform.GetChild(0).GetComponent<Text>().text = menuNames[nameId];

        //set buttons inside
        var newInfo = currentInfo;
        newInfo.TileTypeId = nameId;
        go.GetComponent<TileSpawner>().SetTile(tile, newInfo);


        spawnedMenus.Add(go);
    }
}
