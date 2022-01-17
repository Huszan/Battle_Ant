using UnityEngine;

public class Player
{
    public Color32 Color { get; private set; }
    public float Resources { get; private set; }
    public int UnitCount { get; private set; }
    public int Score { get; private set; }

    public Player(
        float resources,
        int unitCount,
        int initialScore)
    {
        Color = new Color32(
            (byte)Random.Range(0, 255),
            (byte)Random.Range(0, 255),
            (byte)Random.Range(0, 255),
            255
            );
        Resources = resources;
        UnitCount = unitCount;
        Score = initialScore;
    }

    public void AddResources(float amount) => Resources += amount;
    public void SubtractResources(float amount) => Resources -= amount;
    public void AddPoints(int amount) => Score += amount;
    public void SubtractPoints(int amount) => Score -= amount;

    public int UnitLimit()
    {
        return (int)(Resources/10+5);
    }
    public void UnitCreated() => UnitCount++;
    public void UnitDestroyed() => UnitCount--;
}
