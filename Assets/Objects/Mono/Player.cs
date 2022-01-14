using UnityEngine;

public class Player
{
    public Color Color { get; private set; }
    public float Resources { get; private set; }
    public int UnitCount { get; private set; }

    public Player(
        float resources,
        int unitCount)
    {
        Color = new Color(
            Random.Range(0, 255),
            Random.Range(0, 255),
            Random.Range(0, 255)
            );
        Resources = resources;
        UnitCount = unitCount;
    }

    public int UnitLimit()
    {
        return (int)(Resources/10);
    }
    private void UnitCreated()
    {
        UnitCount++;
    }
    private void UnitDestroyed()
    {
        UnitCount--;
    }
}
