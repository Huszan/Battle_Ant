using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAi
{
    public Player Player { get; private set; }
    private int Delay { get; set; }

    public PlayerAi(Player player)
    {
        Player = player;
        switch (GameManager.Instance.Difficulty)
        {
            case (Difficulty.EASY):
                {
                    Delay = 3;
                    break;
                }
            case (Difficulty.NORMAL):
                {
                    Delay = 2;
                    break;
                }
            case (Difficulty.HARD):
                {
                    Delay = 1;
                    break;
                }
            default:
                {
                    Delay = 2;
                    break;
                }
        }
        Debug.Log($"Delay was set for {Delay} seconds");
    }

    // Economy
    public List<GameObject> GatheringHallSpots { get; private set; }
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
    private bool HasResourcesInRange => ResourcesInRange().Count > 0;
    private void FindGatheringHallSpots()
    {
        var list = new List<GameObject>();
        foreach (Building b in ResourcesInRange()){
            var range = Tilemap.Instance.TilesInRange(b.Position, 1)
                .Where(go => Player.BuildRange().Contains(go) &&
                go.GetComponentInChildren<Building>() == null);
            list.AddRange(range);
        }
        GatheringHallSpots = list;
    }

    public IEnumerator Process()
    {
        while (AiManager.Instance.enabled)
        {
            FindGatheringHallSpots();
            if (GatheringHallSpots.Count > 0)
                BuildingManager.Instance.PlaceBuilding(
                GatheringHallSpots[0],
                BuildingManager.Instance.buildings[1],
                Player);
            YELL();
            yield return new WaitForSeconds(Delay);
        }
    }
    public void YELL()
    {
        Debug.Log($"I am an ai. My master is {Player} " +
            $"and i have {GatheringHallSpots.Count} spots to build " +
            $"and also {ResourcesInRange().Count} resources in range, " +
            $"additionaly i can tell you that my owner has {Player.BuildRange().Count} building range overall!");
    }

}

