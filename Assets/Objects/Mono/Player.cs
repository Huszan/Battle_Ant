using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player
{
    public Color32 Color { get; private set; }
    public float Resources { get; private set; }
    public int UnitCount { get; private set; }
    public List<Building> Buildings { get; private set; }

    public Player(float startingResources)
    {
        Color = new Color32(
            (byte)Random.Range(0, 255),
            (byte)Random.Range(0, 255),
            (byte)Random.Range(0, 255),
            255
            );
        Resources = startingResources;
        UnitCount = 0;
        Buildings = new List<Building>();
    }

    public int Score()
    {
        int score = 0;
        foreach (Building building in Buildings)
            score += (int)building.cost * 2;
        return score;
    }
    public void AddResources(float amount) => Resources += amount;
    public void SubtractResources(float amount) => Resources -= amount;

    public int UnitLimit() => (int)(Resources / 10 + 5);
    public bool UnitLimitReached() => UnitCount >= UnitLimit();
    public void UnitCreated() => UnitCount++;
    public void UnitDestroyed() => UnitCount--;

    public List<GameObject> BuildRange()
    {
        var tilesInRange = new List<GameObject>();
        foreach (Building building in Buildings)
            tilesInRange.AddRange(
                Tilemap.Instance.TilesInRange(building.Position, building.range));
        tilesInRange = tilesInRange.Distinct().ToList();
        return tilesInRange;
    }
}
