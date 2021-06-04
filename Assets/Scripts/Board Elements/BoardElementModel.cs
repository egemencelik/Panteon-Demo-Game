using System;
using UnityEngine;

public enum BoardElementState
{
    Moving,
    Stationary
}

public class BoardElementModel : BaseBoardElementComponent
{
    [Header("General Info")]
    public string Name;

    public string Description;
    public Sprite Image;
    public int CellSizeX, CellSizeY;

    [Header("Activity")]
    public float ActivityInterval;

    public int MaterialPerActivity;

    [NonSerialized]
    public BoardElementState State = BoardElementState.Stationary;

    public int TotalCells => CellSizeX * CellSizeY;
    public string Text => $"{Name}\n<size=75%>{Description}";
}