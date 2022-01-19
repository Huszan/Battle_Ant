using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
    [Header("Import")]
    public Image buildingImageField;
    public TMP_Text buildingNameField;
    public TMP_Text buildingDescriptionField;
    public TMP_Text buildingCostField;
    public GameObject PointerSticker;

    private List<GameObject> BuildableBuildings { get; set; }
    private int CurrentBuldingIndex { get; set; }

    private void Start()
    {
        BuildableBuildings = new List<GameObject>();
        foreach (GameObject _building in BuildingManager.Instance.buildings)
        {
            if (_building.GetComponent<Building>().buildable)
                BuildableBuildings.Add(_building);
        }
        CurrentBuldingIndex = 0;
    }

    public void NextBuilding()
    {
        if (CurrentBuldingIndex == BuildableBuildings.Count-1)
            CurrentBuldingIndex = 0;
        else
            CurrentBuldingIndex++;
    }
    public void PreviousBuilding()
    {
        if (CurrentBuldingIndex == 0)
            CurrentBuldingIndex = BuildableBuildings.Count-1;
        else
            CurrentBuldingIndex--;
    }

    private bool RangeIndicatorToggled = false;
    public void ToggleRangeIndicator() => RangeIndicatorToggled = !RangeIndicatorToggled;
    public void ShowRangeIndicator()
    {
        foreach (GameObject go in BuildingManager.Instance.BuildRange(GameManager.Instance.Players[0]))
        {
            if (!RangeIndicatorToggled)
                go.GetComponent<SpriteRenderer>().color = CustomColors.GREEN_TILE;
            else
                go.GetComponent<SpriteRenderer>().color = CustomColors.CHOSEN_TILE;
        }
    }

    private void Update()
    {
        GameObject _building = BuildableBuildings[CurrentBuldingIndex];
        Display(_building);
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag.Equals("Tile"))
                BuildingManager.Instance.PlaceBuilding(
                    hit.collider.gameObject, 
                    _building,
                    GameManager.Instance.Players[0]);
        }
        ShowRangeIndicator();
    }
    private void Display(GameObject buildingToDisplay)
    {
        if (BuildableBuildings.Count > 0)
        {
            buildingImageField.sprite = buildingToDisplay.GetComponent<SpriteRenderer>().sprite;
            Building building = buildingToDisplay.GetComponent<Building>();
            buildingNameField.text = building._name;
            buildingDescriptionField.text = building._description;
            buildingCostField.text = building.cost.ToString();

            PointerSticker.GetComponent<SpriteRenderer>().sprite =
            BuildableBuildings[CurrentBuldingIndex].GetComponent<SpriteRenderer>().sprite;
        }
    }

}