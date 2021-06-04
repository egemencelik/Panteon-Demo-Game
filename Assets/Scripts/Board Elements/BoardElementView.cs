using System;
using UnityEngine;

public class BoardElementView : BaseBoardElementComponent
{
    protected SpriteRenderer spriteRenderer;

    protected BoxCollider2D Collider2D;

    public float CurrentAlpha => spriteRenderer.color.a;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Collider2D = GetComponent<BoxCollider2D>();
    }

    public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    public void SetAlpha(float alpha)
    {
        var color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    public void SetSelected(bool isSelected)
    {
        spriteRenderer.color = isSelected ? Color.grey : Color.white;
    }

    public void SetColliderSize(Vector2 size)
    {
        Collider2D.size = size;
    }
}