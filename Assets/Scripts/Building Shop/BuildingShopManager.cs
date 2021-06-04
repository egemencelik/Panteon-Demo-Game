using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingShopManager : MonoBehaviour
{
    public GameObject Prefab;
    public List<BuildingShopData> Buildings;

    private void Start()
    {
        foreach (var building in Buildings)
        {
            var buildingGO = Instantiate(Prefab, transform);
            var buildingShopItem = buildingGO.GetComponent<BuildingShopItem>();
            buildingShopItem.Initialize(building);
        }
    }
}
