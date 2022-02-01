using System.Collections.Generic;
using UnityEngine;

public class TilemapInfo
{
    private Tilemap Tilemap => Tilemap.Instance;
    public List<GameObject> GoodGatheringHallSpots { get; private set; }

    private List<Building> FoodSources()
    {
        var foodSources = new List<Building>();
        foreach (GameObject go in Tilemap.CreatedTiles)
        {
            Building b = go.GetComponentInChildren<Building>();
            if (b != null && b._name == BuildingManager.Instance.foodSource.GetComponent<Building>()._name)
                foodSources.Add(b);
        }
        return foodSources;
    }
    public List<GameObject> FindGoodGatheringHallSpots()
    {
        var list = new List<GameObject>();

        foreach (Building b in FoodSources())
            list.AddRange(b.TilesInRange());

        return list;
    }
}
