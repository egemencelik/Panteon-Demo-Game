using UnityEngine;

public enum CellState
{
    Default,
    Available,
    Unavailable
}

public class Cell : MonoBehaviour
{
    public CellState State;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    public Tile Tile;

    [SerializeField]
    private Color defaultColor, availableColor, unavaiableColor;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        State = CellState.Default;
    }


    public void SetState(CellState state)
    {
        State = state;
        switch (state)
        {
            case CellState.Available:
                spriteRenderer.color = availableColor;
                break;
            case CellState.Default:
                spriteRenderer.color = defaultColor;
                break;
            case CellState.Unavailable:
                spriteRenderer.color = unavaiableColor;
                break;
            default:
                spriteRenderer.color = defaultColor;
                break;
        }
    }
}