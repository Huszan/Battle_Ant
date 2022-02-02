using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileSorter
{
    public enum Folder
    {
        GATHERING_HALL_SPOTS,
        EDGE_SPOTS,
        DISCARDED_SPOTS,
    }

    private List<GameObject>[] Folders =
        new List<GameObject>[Enum.GetNames(typeof(Folder)).Length];
    public List<GameObject> GetFolder(Folder folder) => Folders[(int)folder];

    public IEnumerator SendNewTiles(List<GameObject> list)
    {
        Sort(list);
        yield return null;
    }

    private void Sort(List<GameObject> list)
    {
        DepositGatheringHallSpots(list);
        DepositUnusedSpots(list);
    }
    private void DepositGatheringHallSpots(List<GameObject> list)
    {
        var folder = Folders[(int)Folder.GATHERING_HALL_SPOTS];
        var goodGatheringHallSpots = Tilemap.Instance.GetInfo.GoodGatheringHallSpots;

        if (folder == null) folder = new List<GameObject>();
        foreach (GameObject go in list)
        {
            if (goodGatheringHallSpots.Contains(go) &&
            go.GetComponentInChildren<Building>() == null &&
            !folder.Contains(go))
            {
                folder.Add(go);
                list.Remove(go);
            }
        }
    }
    private void DepositUnusedSpots(List<GameObject> list)
    {
        var folder = Folders[(int)Folder.DISCARDED_SPOTS];
        if (folder == null) folder = new List<GameObject>();

        foreach (GameObject go in list)
        {
            folder.Add(go);
        }
    }
}
