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

    public override GameObject FindSpotToBuild()
    {
        foreach (GameObject go in Player.Segregator.GetBuildRange)
            if (Tilemap.Instance.GetInfo.GoodGatheringHallSpots.Contains(go) && go.GetComponentInChildren<Building>() == null)
                return go;
        return null;
    }

}
