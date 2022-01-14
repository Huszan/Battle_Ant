using UnityEngine;

public class Building : MonoBehaviour
{
    public string buildingName;
    public float hp;
    public float cost;
    public bool buildable;
    public Player player;

    private bool canBeBuilt()
    {
        if (cost < player.Resources)
            return true;
        else
            return false;
    }
    public void Damage(float amount)
    {
        if (amount < hp)
            hp -= amount;
        else
            Kill();
    }
    private void Kill()
    {
        Destroy(transform.gameObject);
    }
}
