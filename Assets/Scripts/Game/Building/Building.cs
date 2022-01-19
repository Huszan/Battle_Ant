using UnityEngine;

public class Building : MonoBehaviour
{
    public string _name;
    public string _description;
    public float hp;
    public float cost;
    public float range;
    public bool buildable;
    public Player Owner { get; private set; }
    public GameObject Tile { get; private set; }
    
    public Vector2 Position { get; private set; }
    public void SetPosition(Vector2 pos) => Position = pos;
    public void SetOwner(Player player) => Owner = player;

    public void Damage(float amount)
    {
        if (amount < hp)
            hp -= amount;
        else
            Kill();
    }
    private void Kill() => Destroy(transform.gameObject);

}