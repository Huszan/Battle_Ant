using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerLogic : BuildBaseLogic
{
    public TowerLogic(Player player) : base(player)
    {
        BuildingPrefab = BuildingManager.Instance.buildings[2];
        Building = BuildingPrefab.GetComponent<Building>();
    }

    internal override GameObject BuildingPrefab { get; set; }
    internal override Building Building { get; set; }

    public override GameObject FindSpotToBuild()
    {
        int toSkip = 11;
        foreach (GameObject go in Tilemap.Instance.TilesInRange(Player.Segregator.GetBuildings[0].Position, 40))
        {
            if (toSkip > 0) toSkip--;
            else
            {
                if (go.GetComponentInChildren<Building>() == null 
                    && Player.Segregator.GetBuildRange.Contains(go))
                    return go;
                toSkip += Building.range;
            }
        }
        return null;
    }

}
