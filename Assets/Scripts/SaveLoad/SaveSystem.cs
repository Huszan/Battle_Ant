using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private SaveData GatherData()
    {
        SaveData data = new SaveData();
        data.difficulty = GameManager.Instance.Difficulty;
        data.mapSize = Tilemap.Instance.MapSize;
        data.time = GameManager.Instance.TimePassed.Counter;

        foreach (Player player in GameManager.Instance.Players())
        {
            SPlayer sPlayer = new SPlayer();
            sPlayer.color = player.Color;
            sPlayer.resources = player.Resources;
            data.sPlayerList.Add(sPlayer);

            foreach (Building building in player.Segregator.GetBuildings)
            {
                SBuilding sBuilding = new SBuilding();
                if (building.Index() != null)
                    sBuilding.id = (int)building.Index();
                sBuilding.playerId = data.sPlayerList.Count-1;
                sBuilding.position = building.Position;
                data.sBuildingList.Add(sBuilding);
            }
        }
        return data;
    }
    public void SaveDataToFile(string fileName)
    {
        var filePath = "Saves/" + CurrentUser.user.name + "/";
        var fullPath = filePath + fileName;
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        var jsonString = JsonUtility.ToJson(GatherData());
        File.WriteAllText(fullPath, jsonString);
        PopupManager.Instance.Pop(PopupManager.PopType.success, "Game has been saved");
    }
    public SaveData LoadDataFromFile(string fileName)
    {
        var filePath = "Saves/" + CurrentUser.user.name + "/";
        var fullPath = filePath + fileName;
        if (File.Exists(fullPath))
        {
            var jsonString = File.ReadAllText(fullPath);
            return JsonUtility.FromJson<SaveData>(jsonString);
        }
        return null;
    }
}
