using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public SpriteRenderer flagCanvas;

    public string _name;
    public string _description;
    public float hp;
    public float cost;
    public int range;
    public bool buildable;
    public Player Owner { get; private set; }
    public GameObject Tile { get; private set; }
    public Vector2 Position { get; private set; }
    public void SetPosition(Vector2 pos) => Position = pos;
    public void SetOwner(Player player) => Owner = player;
    public List<GameObject> TilesInRange { get; private set; }
    public void SetTilesInRange() => TilesInRange = Tilemap.Instance.TilesInRange(Position, range);
    public GameObject ClosestResourceNode { get; private set; }
    public void SetClosestResourceNode(GameObject node) => ClosestResourceNode = node;

    public void Damage(float amount)
    {
        if (amount < hp)
            hp -= amount;
        else
            Kill();
    }
    private void Kill()
    {
        if (!buildable)
            GameManager.Instance.ClearFromGame(Owner);
        else
        {
            Owner.Segregator.RemoveBuilding(this);
            Destroy(transform.gameObject);
        }
        GameManager.Instance.CheckEndgameCoditions();
    }

}