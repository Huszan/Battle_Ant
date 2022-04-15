using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector2 mapSize;
    public Difficulty difficulty;
    public float time;
    public List<SPlayer> sPlayerList = new List<SPlayer>();
    public List<SBuilding> sBuildingList = new List<SBuilding>();
}

[System.Serializable]
public class SBuilding
{
    public int playerId;
    public int id;
    public Vector2 position;
}

[System.Serializable]
public class SPlayer
{
    public Color32 color;
    public float resources;
}