using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductMenuItem : MonoBehaviour
{
    public ProductData ProductData;
    public BoardBuildingView BuildingView;
    private Button button;
    private Image image;
    private TextMeshProUGUI productText;

    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        productText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(ProductData data, BoardBuildingView view)
    {
        ProductData = data;
        BuildingView = view;
        image.sprite = ProductData.Sprite;
        productText.text = ProductData.ProductName;
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (GameManager.instance.State == PlayerState.Moving) return;
        var obj = ProductData.Create(BuildingView.SpawnPoint.position);
    }
}