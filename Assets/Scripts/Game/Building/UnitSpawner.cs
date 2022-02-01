using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    void Awake()
    {
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
            SpawningBuilding.ClosestResourceNode != null)
        {
            SpawnUnit();
            timer.ResetCounter();
        }
    }

    private void SpawnUnit()
    {
        if (GameManager.Instance.GameState != GameState.PLAYING) return;
        var unit = UnitFactory.GetNewInstance();
        unit.transform.parent = SpawningBuilding.transform;
        unit.gameObject.transform.position = transform.position;
        unit.SetOwner(SpawningBuilding.Owner);
        unit.SetJob(new WorkerJob(SpawningBuilding.ClosestResourceNode.transform.position, unit));
    }
}
