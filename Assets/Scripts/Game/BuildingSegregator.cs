using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingSegregator
{
    public List<Building> Buildings { get; private set; }
    public BuildingSegregator()
    {
        Buildings = new List<Building>();
    }



    public List<GameObject> BuildRange()
    {
        var tilesInRange = new List<GameObject>();
        foreach (Building building in Buildings)
            tilesInRange.AddRange(
                building.TilesInRange);
        tilesInRange = tilesInRange.Distinct().ToList();
        return tilesInRange;
    }
}
