using UnityEngine;

public class Building : MonoBehaviour
{
    public string _name;
    public string _description;
    public float hp;
    public float cost;
    public float range;
    public bool buildable;

    public Player player;
    public void SetOwner(Player player)
    {
        this.player = player;
        gameObject.transform.GetComponent<SpriteRenderer>().color = player.Color;
    }
    public Player GetOwner() => player;

    private bool CanBeBuilt(Player player)
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