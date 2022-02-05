using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player
{
    public Color32 Color { get; private set; }
    public float Resources { get; private set; }
    public BuildingSegregator Segregator { get; private set; }
    public PlayerAi PlayerAi { get; private set; }
    public int UnitCount()
    {
        int count = 0;
        foreach (Building building in Segregator.GetBuildings)
            foreach (Transform child in building.gameObject.transform)
                if (child.CompareTag("Unit")) count++;
        return count;
    }
    public bool IsDefeated()
    {
        if (Segregator.GetBuildings.Count <= 0) return true;
        foreach (Building building in Segregator.GetBuildings)
        {
            if (building.buildable == false)
                return false;
        }
        return true;
    }
    public void AddAi() => PlayerAi = new PlayerAi(this);

    public Player(float startingResources)
    {
        Color = new Color32(
            (byte)Random.Range(0, 255),
            (byte)Random.Range(0, 255),
            (byte)Random.Range(0, 255),
            255
            );
        Resources = startingResources;
        Segregator = new BuildingSegregator();
    }

    public int Score()
    {
        int score = 0;
        foreach (Building building in Segregator.GetBuildings)
            score += (int)building.cost;
        score += (int)Resources / 5;
        return score * ((int)GameManager.Instance.Difficulty + 1);
    }
    public void AddResources(float amount) => Resources += amount;
    public void SubtractResources(float amount) => Resources -= amount;

    public int UnitLimit() => (int)(Resources / 10 + 5);
    public bool UnitLimitReached() => UnitCount() >= UnitLimit();

}
