﻿using UnityEngine;

public class Tilemap : MonoBehaviour
{  
    [Header("Import")]
    [SerializeField]
    private GameObject tileGrass;
    public Vector3 TileSize { get; private set; }
    public GameObject[,] CreatedTiles { get; private set; }

    private void Awake()
    {
        TileSize = tileGrass.GetComponent<Renderer>().bounds.size;
    }

    public Vector2 MapSize => new Vector2(
        CreatedTiles.GetLength(0), 
        CreatedTiles.GetLength(1));
    public Vector2 TransformSize => new Vector2(
            MapSize.x * TileSize.x,
            MapSize.y * TileSize.y);
    public GameObject[] Corners() => new GameObject[]
    {
        CreatedTiles[1, 1],
        CreatedTiles[(int)MapSize.x-2, (int)MapSize.y-2],
        CreatedTiles[1, (int)MapSize.y-2],
        CreatedTiles[(int)MapSize.x-2, 1]
    };
    private bool MapSizeIsViable(Vector2 size) => size.x > 0 || size.y > 0;
    private bool MapIsGenerated() => CreatedTiles != null;
    public void SwitchOutline()
    {
        if (!MapIsGenerated()) 
        {
            Debug.LogError("Map is not generated!");
            return; 
        }
        

        foreach (GameObject tile in CreatedTiles)
        {
            if(tile.transform.childCount > 0)
            {
                GameObject outline = tile.transform.Find("Outline").gameObject;
                if (outline.activeSelf == false) outline.SetActive(true);
                else outline.SetActive(false);
            }
        }
    }
    public void GenerateTilemap(Vector2 mapSize)
    {
        if (!MapSizeIsViable(mapSize))
        {
            Debug.Log($"{name} : you can't create tilemap for this map size => {mapSize}");
            return;
        }
        /* Create, rename, set parent and store tiles */
        CreatedTiles = new GameObject[(int)mapSize.x, (int)mapSize.y];
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                GameObject tile = Instantiate(tileGrass);
                tile.transform.rotation = Quaternion.identity;

                if (i % 2 == 0)
                    tile.transform.position = new Vector3(
                        (float)(j * (TileSize.x)),
                        (float)(i * (TileSize.y * 0.75 )), 0);
                if (i % 2 == 1)
                    tile.transform.position = new Vector3(
                        (float)(j * (TileSize.x) + (TileSize.x / 2)),
                        (float)(i * (TileSize.y * 0.75)), 0);

                tile.name = i.ToString() + "." + j.ToString();
                tile.transform.SetParent(transform);
                CreatedTiles[i, j] = tile;
            }
        }
        Debug.Log("Map was created");
    }

}
