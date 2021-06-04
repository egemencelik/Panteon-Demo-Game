using UnityEngine;

[CreateAssetMenu(fileName = "Soldier", menuName = "Product/Soldier")]
public class SoldierData : ProductData
{
    public override MonoBehaviour Create(Vector2 pos)
    {
        var soldierController = GameManager.instance.SoldierFactory.GetInstance();
        soldierController.transform.position = pos;
        soldierController.Spawn();
        return soldierController;
    }
}