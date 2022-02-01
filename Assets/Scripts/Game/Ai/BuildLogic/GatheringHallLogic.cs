using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GatheringHallLogic : BuildBaseLogic
{
    public GatheringHallLogic(Player player) : base(player)
    {
        BuildingPrefab = BuildingManager.Instance.buildings[1];
        Building = BuildingPrefab.GetComponent<Building>();
    }

    internal override GameObject BuildingPrefab { get; set; }
    internal override Building Building { get; set; }

    public override void FindSpotsToBuild()
    {
        var list = new List<GameObject>();
        foreach (Building b in ResourcesInRange())
        {
            var range = Tilemap.Instance.TilesInRange(b.Position, 1)
                .Where(go => Player.BuildRange().Contains(go) &&
                go.GetComponentInChildren<Building>() == null);
            list.AddRange(range);
        }
        SpotsToBuild = list;
    }
    private List<Building> ResourcesInRange()
    {
        var list = new List<Building>();
        foreach (GameObject go in Player.BuildRange())
        {
            Building b = go.GetComponentInChildren<Building>();
            if (b != null && b._name == BuildingManager.Instance.foodSource.GetComponent<Building>()._name)
                list.Add(b);
        }
        return list;
    }
}
