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
        conditions.Add(SpotsToBuild.Count > 0);
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

    public List<GameObject> SpotsToBuild { get; set; }
    public abstract void FindSpotsToBuild();
    public void Build()
    {
        BuildingManager.Instance.PlaceBuilding(
            SpotsToBuild[0],
            BuildingPrefab,
            Player);
    }
    public void YELLINFO()
    {
        Debug.Log($"I am an ai. My master is {Player} {Player.Color} " +
            $"and i have {SpotsToBuild.Count} spots to build " +
            $"additionaly i can tell you that my owner has {Player.BuildRange().Count} building range overall!");
    }
}
