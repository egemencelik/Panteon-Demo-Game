using UnityEngine;

public abstract class ProductData : ScriptableObject
{
    public string ProductName;
    public Sprite Sprite;

    public abstract MonoBehaviour Create(Vector2 pos);
}