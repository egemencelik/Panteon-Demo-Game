using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBuildingView : BoardElementView
{
    public Transform SpawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (boardElement.Model.State == BoardElementState.Stationary) return;

        if (other.CompareTag("Cell"))
        {
            var controller = (BoardBuildingController) boardElement.Controller;
            var cell = other.GetComponent<Cell>();
            controller.OnCellEnter(cell);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (boardElement.Model.State == BoardElementState.Stationary) return;

        if (other.CompareTag("Cell"))
        {
            var controller = (BoardBuildingController) boardElement.Controller;
            var cell = other.GetComponent<Cell>();
            controller.OnCellExit(cell);
        }
    }
}