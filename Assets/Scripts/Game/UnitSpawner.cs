using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    void Awake() {
        SpawningBuilding = gameObject.GetComponent<Building>();
        if (SpawningBuilding == null)
            Debug.LogError($"{name} need a Building class to work");
        timer.StartCounting();
        UnitFactory = FindObjectOfType<UnitFactory>();
    }

    public float spawnRatePerMinute;
    [SerializeField]
    private Timer timer;

    private UnitFactory UnitFactory { get; set; }
    private Building SpawningBuilding { get; set; }

    private void Update()
    {
        if (timer.ReachedTreshold(60 / spawnRatePerMinute) && !SpawningBuilding.Owner.UnitLimitReached())
        {
            SpawnUnit();
            timer.ResetCounter();
        }
    }

    private void SpawnUnit()
    {
        var go = UnitFactory.GetNewInstance();
        var unit = go.GetComponent<Unit>();
        go.transform.position = transform.position;
        unit.SetOwner(SpawningBuilding.Owner);
        SpawningBuilding.Owner.UnitCreated();
    }

}
