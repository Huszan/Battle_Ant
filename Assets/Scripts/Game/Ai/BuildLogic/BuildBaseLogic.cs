using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BuildBaseLogic
{
    public Player Player { get; private set; }
    internal abstract GameObject BuildingPrefab { get; set; }
    internal abstract Building Building { get; set; }
    public BuildBaseLogic(Player player)
    {
        Player = player;
    }

    private List<bool> Conditions()
    {
        var conditions = new List<bool>();
        conditions.Add(SpotToBuild != null);
        conditions.Add(Building.cost < Player.Resources);
        conditions.Add(Player.UnitCount() + (int)Building.cost / 10 * 2 < Player.UnitLimit());
        return conditions;
    }
    public virtual List<bool> AdditionalConditions()
    {
        var conditions = new List<bool>();
        conditions.Add(true);
        return conditions;
    }
    public bool ConditionsMet() => Conditions().All(c => c == true) && AdditionalConditions().All(c => c == true);

    public GameObject SpotToBuild { get; set; }
    public abstract void FindSpotToBuild();
    public void Build()
    {
        BuildingManager.Instance.PlaceBuilding(
            SpotToBuild,
            BuildingPrefab,
            Player);
        SpotToBuild = null;
    }
}
