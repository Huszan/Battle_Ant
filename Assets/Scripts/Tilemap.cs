using System;
using UnityEngine;

public class Tilemap : MonoBehaviour
{
    /* Map Size */
    private Vector2 mapSize;
    /* Tiles */
    GameObject[] createdTiles;
    /* Import */
    public GameObject tileGrass;
    public GameObject tileOOB;

    private Vector3 tileSize;

    public void SetMapSize(Vector2 mapSize)
    {
        this.mapSize.x = mapSize.x;
        this.mapSize.y = mapSize.y;
        tileSize = tileGrass.GetComponent<Renderer>().bounds.size;
    }
    private void StoreTiles()
    {
        int tileAmount = (int)mapSize.x * (int)mapSize.y;
        createdTiles = new GameObject[tileAmount];

        int tileCounter = 0;
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                createdTiles[tileCounter++] = GameObject.Find(i.ToString() + "." + j.ToString());
            }
        }
    }
    private bool MapSizeIsViable()
    {
        if (mapSize.x <= 0 && mapSize.y <= 0)  return false;
        if (mapSize.x > 0 && mapSize.y > 0) return true;

        print(this.name + " : something went wrong");
        return false;
    }
    private bool MapIsGenerated()
    {
        if (createdTiles == null)
            return false;
        else
            return true;
    }

    public void SwitchOutline()
    {
        if (!MapIsGenerated()) 
        {
            PopupManager.PopError("Map is not generated!");
            return; 
        }
        

        foreach (GameObject tile in createdTiles)
        {
            if(tile.transform.childCount > 0)
            {
                GameObject child = tile.transform.Find("Outline").gameObject;
                if (child.activeSelf == false) child.SetActive(true);
                else child.SetActive(false);
            }
        }
    }
    public void GenerateTilemap(Vector2 mapSize)
    {
        SetMapSize(mapSize);

        /* Create tiles, rename them and set parent */
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                if (MapSizeIsViable() == false) break;
                else if (!(i == 0 && j == 0 || i == mapSize.y - 1 && j == mapSize.x - 1))
                {
                    GameObject tile;
                    if (j == 0 || j == mapSize.x - 1 || i == 0 || i == mapSize.y - 1)
                        tile = Instantiate(tileOOB);
                    else
                        tile = Instantiate(tileGrass);
                    tile.transform.rotation = Quaternion.identity;

                    if (i % 2 == 0)
                        tile.transform.position = new Vector3(
                            (float)(j * (tileSize.x)),
                            (float)(i * (tileSize.y * 0.75 )), 0);
                    if (i % 2 == 1)
                        tile.transform.position = new Vector3(
                            (float)(j * (tileSize.x) + (tileSize.x / 2)),
                            (float)(i * (tileSize.y * 0.75)), 0);

                    tile.name = i.ToString() + "." + j.ToString();
                    tile.transform.SetParent(transform);
                }
            }

        }
        MainCameraManager.CenterToMap(mapSize, tileSize);
        StoreTiles();
        PopupManager.PopInfo("Map created!");
    }

}
