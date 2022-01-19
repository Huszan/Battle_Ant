using UnityEngine;

public class Tilemap : MonoBehaviour
{  
    [Header("Import")]
    [SerializeField]
    private GameObject tileGrass;

    public Vector2 MapSize { get; private set; }
    public Vector3 TileSize { get; private set; }
    public GameObject[,] CreatedTiles { get; private set; }

    private void Awake()
    {
        TileSize = tileGrass.GetComponent<Renderer>().bounds.size;
    }
    public void SetMapSize(Vector2 mapSize)
    {
        MapSize = mapSize;
        CreatedTiles = new GameObject[(int)mapSize.x, (int)mapSize.y];
    }
    public Vector2 GetTransformSize()
    {
        return new Vector2(
            MapSize.x * TileSize.x,
            MapSize.y * TileSize.y);
    }
    public GameObject[] GetCorners() => new GameObject[] 
    { 
        CreatedTiles[1, 1],
        CreatedTiles[(int)MapSize.x-2, (int)MapSize.y-2],
        CreatedTiles[1, (int)MapSize.y-2],
        CreatedTiles[(int)MapSize.x-2, 1]
    };
    private void StoreTiles()
    {
        for (int i = 0; i < MapSize.y; i++)
        {
            for (int j = 0; j < MapSize.x; j++)
            {
                CreatedTiles[i,j] = GameObject.Find(i.ToString() + "." + j.ToString());
            }
        }
    }
    private bool MapSizeIsViable()
    {
        if (MapSize.x <= 0 && MapSize.y <= 0)  return false;
        if (MapSize.x > 0 && MapSize.y > 0) return true;

        print(name + " : something went wrong");
        return false;
    }
    private bool MapIsGenerated()
    {
        if (CreatedTiles == null)
            return false;
        else
            return true;
    }

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
                    tile = Instantiate(tileGrass);
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
                }
            }

        }
        StoreTiles();
        Debug.Log("Map was created");
    }

}
