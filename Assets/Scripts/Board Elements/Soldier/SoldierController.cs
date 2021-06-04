using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoldierController : BoardElementController
{
    public Cell CurrentCell { get; set; }
    public List<Cell> Path { get; set; }
    private WaitForSeconds waitForSeconds;
    private SoldierModel model => (SoldierModel) boardElement.Model;

    private int currentCellIndex;

    protected override void Awake()
    {
        base.Awake();
        waitForSeconds = new WaitForSeconds(model.WaitDelay);
    }

    private void Start()
    {
    }

    public void Spawn()
    {
        try
        {
            CurrentCell = Utils.GetGameObjectAtLocation(transform.position, "Cell").collider.GetComponent<Cell>();
            if (CurrentCell.State == CellState.Unavailable)
            {
                Debug.Log("Spawn point collides with another building!");
                gameObject.SetActive(false);
                return;
            }
            transform.position = CurrentCell.transform.position;
            GameManager.instance.Soldiers++;
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Spawn point outside of map!");
            gameObject.SetActive(false);
            return;
        }

        model.MoveSpeed = Random.Range(1, 5);
        ElementActivity();
    }

    public void StartPath(List<Cell> path)
    {
        Path = path.ToList();
        currentCellIndex = 0;
        StartCoroutine(GoToNextCell());
    }

    private void ResetPath()
    {
        foreach (var cell in Path.ToList())
        {
            cell.SetState(CellState.Default);
            Path.Remove(cell);
        }
    }

    private IEnumerator GoToNextCell()
    {
        while (currentCellIndex < Path.Count)
        {
            transform.position = Path[currentCellIndex].transform.position;
            CurrentCell = Path[currentCellIndex];
            currentCellIndex++;
            yield return waitForSeconds;
        }

        ResetPath();
    }

    protected override void ElementActivity()
    {
        base.ElementActivity();
        if (GameManager.instance.Food < 1)
        {
            Debug.Log("Soldier died because there is no food!");
            GameManager.instance.Soldiers--;
            gameObject.SetActive(false);
            return;
        }

        GameManager.instance.Food -= model.MaterialPerActivity;
    }
}