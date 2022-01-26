using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Building BuildingShooting;
    public float damage;
    public float shotsPerMinute;

    private Timer Timer { get; set; }

    private void Awake()
    {
        Timer = gameObject.AddComponent<Timer>();
        Timer.StartCounting();
    }
    private void Update()
    {
        if (Timer.Counter > 60f / shotsPerMinute)
        {
            Shoot(RandomTarget());
            Timer.ResetCounter();
        }   
    }

    private Building RandomTarget()
    {
        var possibleTargets = new List<Building>();
        foreach (GameObject tile in BuildingShooting.TilesInRange())
        {
            var building = tile.GetComponentInChildren<Building>();
            if (building != null && building.Owner != null && building.Owner != BuildingShooting.Owner)
                possibleTargets.Add(building);
        }
        if (possibleTargets.Count > 0)
            return possibleTargets[0];
        else
            return null;
    }
    private void Shoot(Building target)
    {
        if (target == null) return;

        target.Damage(damage);
    }
}
