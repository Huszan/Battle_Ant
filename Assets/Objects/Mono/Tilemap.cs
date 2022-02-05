using System.Collections.Generic;
using UnityEngine;

public class Tilemap : MonoBehaviour
{
    public static Tilemap Instance { get; private set; }
    public Info GetInfo = new Info();
    [Header("Import")]
    [SerializeField]
    private GameObject tileGrass;
    public Vector3 TileSize { get; private set; }
    public GameObject[,] CreatedTiles { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        TileSize = tileGrass.GetComponent<Renderer>().bounds.size;
    }

    public Vector2 MapSize => new Vector2(
        CreatedTiles.GetLength(0),
        CreatedTiles.GetLength(1));
    public Vector2 TransformSize => new Vector2(
            MapSize.x * TileSize.x,
            MapSize.y * TileSize.y);
    public Vector2 TileCoordinates(GameObject searchedTile)
    {
        for (int i = 0; i < CreatedTiles.GetLength(0); i++)
        {
            for (int j = 0; j < CreatedTiles.GetLength(1); j++)
            {
                if (CreatedTiles[i, j] == searchedTile)
                    return new Vector2(i, j);
            }
        }
        return new Vector2();
    }
    public GameObject[] PlayerCorners() => new GameObject[]
    {
        CreatedTiles[1, 1],
        CreatedTiles[(int)MapSize.x-2, (int)MapSize.y-2],
        CreatedTiles[1, (int)MapSize.y-2],
        CreatedTiles[(int)MapSize.x-2, 1]
    };
    public GameObject[] FoodCorners() => new GameObject[]
{
        CreatedTiles[0, 0],
        CreatedTiles[(int)MapSize.x-1, (int)MapSize.y-1],
        CreatedTiles[1, (int)MapSize.y-1],
        CreatedTiles[(int)MapSize.x-1, 0]
};

    private enum JumpDirection
    {
        SW, W, NW, NE, E, SE
    }
    public List<GameObject> TilesInRange(Vector2 pos, int range)
    {
        var tiles = new List<GameObject>();

        Vector2 pointer;
        for (int r = 1; r <= range; r++)
        {
            pointer = pos + new Vector2(0, r);
            JumpAndGrab(JumpDirection.SW, r);
            JumpAndGrab(JumpDirection.W, r);
            JumpAndGrab(JumpDirection.NW, r);
            JumpAndGrab(JumpDirection.NE, r);
            JumpAndGrab(JumpDirection.E, r);
            JumpAndGrab(JumpDirection.SE, r);
        }

        return tiles;

        void JumpAndGrab(JumpDirection direction, int repeatCount)
        {
            for (int i = 0; i < repeatCount; i++)
            {
                switch (direction)
                {
                    case (JumpDirection.SW):
                        {
                            if (pointer.x % 2 == 0)
                                pointer += new Vector2(-1, -1);
                            else
                                pointer += new Vector2(-1, 0);
                            break;
                        }
                    case (JumpDirection.W):
                        {
                            pointer += new Vector2(0, -1);
                            break;
                        }
                    case (JumpDirection.NW):
                        {
                            if (pointer.x % 2 == 0)
                                pointer += new Vector2(1, -1);
                            else
                                pointer += new Vector2(1, 0);
                            break;
                        }
                    case (JumpDirection.NE):
                        {
                            if (pointer.x % 2 == 0)
                                pointer += new Vector2(1, 0);
                            else
                                pointer += new Vector2(1, 1);
                            break;
                        }
                    case (JumpDirection.E):
                        {
                            pointer += new Vector2(0, 1);
                            break;
                        }
                    case (JumpDirection.SE):
                        {
                            if (pointer.x % 2 == 0)
                                pointer += new Vector2(-1, 0);
                            else
                                pointer += new Vector2(-1, 1);
                            break;
                        }
                }
                if (PositionExists(pointer))
                {
                    var tile = CreatedTiles[(int)pointer.x, (int)pointer.y];
                    tiles.Add(tile);
                }
            }
        }
    }
    public List<GameObject> GetTilesFromPositions(List<Vector2> positions)
    {
        List<GameObject> tiles = new List<GameObject>();
        foreach (Vector2 pos in positions)
        {
            if (PositionExists(pos))
                tiles.Add(CreatedTiles[
                    (int)pos.x, (int)pos.y]);
        }
        return tiles;
    }
    private bool PositionExists(Vector2 pos) => pos.x >= 0 && pos.y >= 0 && pos.x < MapSize.x && pos.y < MapSize.y;
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
            if (tile.transform.childCount > 0)
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
        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                GameObject tile = Instantiate(tileGrass);
                tile.transform.rotation = Quaternion.identity;

                if (i % 2 == 0)
                    tile.transform.position = new Vector3(
                        (float)(j * (TileSize.x)),
                        (float)(i * (TileSize.y * 0.75)), 0);
                if (i % 2 == 1)
                    tile.transform.position = new Vector3(
                        (float)(j * (TileSize.x) + (TileSize.x / 2)),
                        (float)(i * (TileSize.y * 0.75)), 0);

                tile.name = new Vector2(i, j).ToString();
                tile.transform.SetParent(transform);
                CreatedTiles[i, j] = tile;
            }
        }
        Debug.Log("Map was created");
    }

    public class Info
    {
        public List<Building> FoodSources { get; private set; }
        public List<GameObject> GoodGatheringHallSpots { get; private set; }

        public void Gather()
        {
            FoodSources = FindFoodSources();
            GoodGatheringHallSpots = FindGoodGatheringHallSpots();
        }

        private List<Building> FindFoodSources()
        {
            var foodSources = new List<Building>();
            foreach (GameObject go in Instance.CreatedTiles)
            {
                Building b = go.GetComponentInChildren<Building>();
                if (b != null && b._name == BuildingManager.Instance.foodSource.GetComponent<Building>()._name)
                    foodSources.Add(b);
            }
            return foodSources;
        }
        private List<GameObject> FindGoodGatheringHallSpots()
        {
            var list = new List<GameObject>();

            foreach (Building b in FoodSources)
                list.AddRange(Instance.TilesInRange(b.Position, 1));

            return list;
        }
    }
}
