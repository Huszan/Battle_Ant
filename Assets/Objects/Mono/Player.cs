using System.Collections.Generic;
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
    public void UnitCreated() => UnitCount++;
    public void UnitDestroyed() => UnitCount--;
}
