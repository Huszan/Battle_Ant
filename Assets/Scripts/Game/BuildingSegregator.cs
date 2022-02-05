using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingSegregator
{
    private List<Building> Buildings { get; set; }
    public List<Building> GetBuildings => Buildings;
    public void AddBuilding(Building building)
    {
        Buildings.Add(building);
        BuildRange.AddRange(building.TilesInRange);
    }
    public void RemoveBuilding(Building building)
    {
        Buildings.Remove(building);
        foreach (GameObject go in building.TilesInRange) 
            BuildRange.Remove(go);
    }
    private List<GameObject> BuildRange { get; set; }
    public List<GameObject> GetBuildRange => BuildRange.Distinct().ToList();

    public BuildingSegregator()
    {
        Buildings = new List<Building>();
        BuildRange = new List<GameObject>();
    }

}
