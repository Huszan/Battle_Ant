using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [Header("Import")]
    public GameObject[] buildings;
    public GameObject foodSource;

    public static BuildingManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaceBuilding(GameObject tileGO, GameObject buildingGO, Player player, bool conditioned = true)
    {
        Building buildingPref = buildingGO.GetComponent<Building>();

        if (conditioned && !BuildConditionsPassed(tileGO, buildingPref, player))
            return;

        var obj =
        Instantiate(
            buildingGO,
            tileGO.transform.position,
            new Quaternion(0, 0, 0, 0),
            tileGO.transform);
        var building =
            obj.GetComponent<Building>();

        building.SetOwner(player);
        building.SetPosition(Tilemap.Instance.TileCoordinates(tileGO));
        if (player != null)
        {
            player.SubtractResources(buildingPref.cost);
            player.Buildings.Add(building);
            obj.transform.GetComponent<SpriteRenderer>().color = player.Color;
        }
    }

    private bool BuildConditionsPassed(GameObject tileGO, Building buildingPref, Player player)
    {
        bool poping = true;
        if (player != GameManager.Instance.HumanPlayer) poping = false;
        if (tileGO.GetComponentInChildren<Building>() != null)
        {
            if (poping) PopupManager.Instance.Pop(
                PopupManager.PopType.warning,
                "You can't place building inside another building");
            return false;
        }
        if (buildingPref.cost > player.Resources)
        {
            if (poping) PopupManager.Instance.Pop(
                PopupManager.PopType.warning,
                "You can't have enough resources");
            return false;
        }
        if (!player.BuildRange().Contains(tileGO))
        {
            if (poping) PopupManager.Instance.Pop(
                PopupManager.PopType.warning,
                "You can't place it here, expand your build range");
            return false;
        }
        return true;
    }
}
