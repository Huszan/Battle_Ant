﻿using System.Collections.Generic;
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

    public List<GameObject> BuildRange(Player player)
    {
        var tiles = Tilemap.Instance.CreatedTiles;
        var tilesInRange = new List<GameObject>();
        foreach (Building building in player.Buildings)
        {
            for (int i = (int)building.Position.x - (int)building.range; i <= building.range + (int)building.Position.x; i++)
            {
                for (int j = (int)building.Position.y - (int)building.range; j <= building.range + (int)building.Position.y; j++)
                {
                    if (i >= 0 && j >= 0 && i < tiles.GetLength(0) && j < tiles.GetLength(1) && !tilesInRange.Contains(tiles[i, j]))
                        tilesInRange.Add(tiles[i, j]);
                }
            }
        }
        return tilesInRange;
    }
    public void PlaceBuilding(GameObject tileGO, GameObject buildingGO, Player player, bool conditioned = true)
    {
        Building buildingPref = buildingGO.GetComponent<Building>();

        if (conditioned)
        {
            if (tileGO.GetComponentInChildren<Building>() != null)
            {
                PopupManager.Instance.Pop(
                    PopupManager.PopType.warning,
                    "You can't place building inside another building");
                return;
            }
            if (buildingPref.cost > player.Resources)
            {
                PopupManager.Instance.Pop(
                    PopupManager.PopType.warning,
                    "You can't have enough resources");
                return;
            }
            if (!BuildRange(player).Contains(tileGO))
            {
                PopupManager.Instance.Pop(
                    PopupManager.PopType.warning,
                    "You can't place it here, expand your build range");
                return;
            }
        }

        player.SubtractResources(buildingPref.cost);

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
        player.Buildings.Add(building);
        obj.transform.GetComponent<SpriteRenderer>().color = player.Color;
        Debug.Log($"{building} was created for {player.Color}, it's coordinates -> {building.Position}");
    }
}
