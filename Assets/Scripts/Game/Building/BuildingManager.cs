using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [Header("Import")]
    public GameObject[] buildings;

    public static BuildingManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaceBuilding(GameObject tileGO, GameObject buildingGO, Player player)
    {
        Building building = buildingGO.GetComponent<Building>();
        if (tileGO.GetComponentInChildren<Building>() != null)
        {
            PopupManager.Instance.Pop(
                PopupManager.PopType.warning,
                "You can't place building inside another building");
            return;
        }
        if (building.cost > player.Resources)
        {
            PopupManager.Instance.Pop(
                PopupManager.PopType.warning,
                "You can't have enough resources");
            return;
        }

        player.SubtractResources(building.cost);
        player.AddPoints((int)building.cost * 2);
        GameObject obj = Instantiate(
            buildingGO, 
            tileGO.transform.position, 
            new Quaternion(0,0,0,0), 
            tileGO.transform);

        obj.GetComponent<Building>().SetOwner(player);
    }
}
