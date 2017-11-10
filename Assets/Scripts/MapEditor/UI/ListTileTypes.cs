using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListTileTypes : MonoBehaviour
{

    [SerializeField]
    GameObject showPrefab;

    public static ListTileTypes Instance { get; private set; }

    List<GameObject> spawnedMenus = new List<GameObject>();

    static string[] menuNames = new string[] { "Wall tiles", "Floor tiles", "Ceiling tiles", "Traps", "Puzzles", "Other" };


    private void Start()
    {
        Instance = this;
    }

    public void ShowTypesOfTile(BlockTypeSO block, int variation = 0)
    {
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
        go.GetComponent<TileSpawner>().SetTile(tile);

        spawnedMenus.Add(go);
    }

    void AddMenu(GameObject[] tile, int nameId)
    {
        var go = Instantiate(showPrefab);
        go.transform.SetParent(transform);
        go.transform.GetChild(0).GetComponent<Text>().text = menuNames[nameId];

        //set buttons inside
        go.GetComponent<TileSpawner>().SetTile(tile);


        spawnedMenus.Add(go);
    }
}
