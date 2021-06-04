using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class BuildingShopData : ScriptableObject
{
   public string BuildingName;
   public Sprite Sprite;
   public GameObject Prefab;
}
