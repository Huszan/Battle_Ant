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
        if (
            timer.ReachedTreshold(60 / spawnRatePerMinute) && 
            !SpawningBuilding.Owner.UnitLimitReached() && 
            SpawningBuilding.FindClosestResources() != null)
        {
            SpawnUnit();
            timer.ResetCounter();
        }
    }

    private void SpawnUnit()
    {
        var unit = UnitFactory.GetNewInstance();
        unit.gameObject.transform.position = transform.position;
        unit.SetOwner(SpawningBuilding.Owner);
        unit.SetJob(new WorkerJob(SpawningBuilding.FindClosestResources().transform.position, unit));
        SpawningBuilding.Owner.UnitCreated();
    }
    //SpawningBuilding.ClosestResource()
}
