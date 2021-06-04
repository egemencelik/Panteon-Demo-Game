using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardBuildingController : BoardElementController
{
    private readonly HashSet<Cell> takenCells = new HashSet<Cell>();
    private readonly HashSet<Cell> collidedUnavailableCells = new HashSet<Cell>();


    protected override void OnCreate()
    {
        base.OnCreate();
        SnapPosition();
        SetCellStates(CellState.Unavailable);
        SetState(BoardElementState.Stationary);
        boardElement.View.SetAlpha(1);
        GameManager.instance.State = PlayerState.Normal;
    }

    private void FixedUpdate()
    {
        if (boardElement.Model.State == BoardElementState.Moving)
        {
            boardElement.View.SetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    protected override void Update()
    {
        base.Update();

        if (boardElement.Model.State == BoardElementState.Moving)
        {
            if (Math.Abs(boardElement.View.CurrentAlpha - .4f) > 0.1f)
            {
                boardElement.View.SetAlpha(.4f);
            }

            // Left click to build
            if (Input.GetMouseButtonDown(0))
            {
                if (takenCells.Count != boardElement.Model.TotalCells) return;
                OnCreate();
            }

            // Right click to cancel
            if (Input.GetMouseButtonDown(1))
            {
                SetCellStates(CellState.Default);
                Destroy(gameObject);
            }
        }
    }

    public void OnCellEnter(Cell c)
    {
        if (c.State == CellState.Unavailable)
        {
            collidedUnavailableCells.Add(c);
            SetCellStates(CellState.Unavailable);
            return;
        }

        takenCells.Add(c);
        c.SetState(CellState.Available);
    }

    public void OnCellExit(Cell c)
    {
        if (c.State == CellState.Unavailable)
        {
            collidedUnavailableCells.Remove(c);

            if (collidedUnavailableCells.Count == 0)
            {
                SetCellStates(CellState.Available);
            }
        }

        if (takenCells.Contains(c))
        {
            takenCells.Remove(c);
            c.SetState(CellState.Default);
        }
    }


    // Centers position of the building to make it align with the grid
    private void SnapPosition()
    {
        var sumX = 0f;
        var sumY = 0f;
        var count = takenCells.Count;
        foreach (var takenCell in takenCells)
        {
            sumX += takenCell.transform.position.x;
            sumY += takenCell.transform.position.y;
        }

        boardElement.View.SetPosition(new Vector2(sumX / count, sumY / count));
    }

    public void SetCellStates(CellState state)
    {
        foreach (var takenCell in takenCells)
        {
            takenCell.SetState(state);
        }
    }
}