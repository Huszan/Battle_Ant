using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public string _name;
    public float hp;
    public float cost;
    public float movSpeed;
    public int carryAmount;
    public Player Owner { get; private set; }

    public void Damage(float amount)
    {
        if (amount < hp)
            hp -= amount;
        else
            Kill();
    }
    private void Kill()
    {
        Owner.UnitDestroyed();
        Destroy(transform.gameObject);
    }
    public void SetOwner(Player player) => Owner = player;
}
