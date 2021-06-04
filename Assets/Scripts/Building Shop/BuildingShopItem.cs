using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingShopItem : MonoBehaviour
{
    public BuildingShopData BuildingData;

    private Button button;
    private Image image;
    private TextMeshProUGUI buildingText;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        buildingText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(BuildingShopData data)
    {
        BuildingData = data;
        image.sprite = BuildingData.Sprite;
        buildingText.text = BuildingData.BuildingName;
        button.onClick.AddListener(OnClick);
    }
    
    private void OnClick()
    {
        if (GameManager.instance.State == PlayerState.Moving) return;
        
        var boardBuilding = Instantiate(BuildingData.Prefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        boardBuilding.GetComponent<BoardBuildingController>().Initialize();
        GameManager.instance.State = PlayerState.Moving;
    }
}