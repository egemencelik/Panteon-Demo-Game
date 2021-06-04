using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public enum PlayerState
{
    Normal,
    Moving,
    SelectingBuilding,
    SelectingSoldier,
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SoldierFactory SoldierFactory;

    [SerializeField]
    private TextMeshProUGUI text;

    public PlayerState State { get; set; }

    public int PowerLevel { get; set; }
    public int Woods { get; set; }
    public int Soldiers { get; set; }
    public int Stones { get; set; }
    public int Food { get; set; }

    private Cell start, finish;
    private SoldierController selectedSoldier;
    private Camera mainCamera;
    private readonly WaitForSeconds waitForSeconds = new WaitForSeconds(1);


    private void Awake()
    {
        instance = this;
        // save main camera to only find it with tag once
        mainCamera = Camera.main;
        State = PlayerState.Normal;
    }

    private void Start()
    {
        StartCoroutine(UpdateTextEverySecond());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            GetGameObjectAtPosition();
    }

    private IEnumerator UpdateTextEverySecond()
    {
        while (true)
        {
            yield return waitForSeconds;
            UpdateText();
        }
    }

    public void UpdateText()
    {
        text.text = $"Power Level: {PowerLevel}<space=1em> Woods: {Woods}<space=1em> Stones: {Stones}<space=1em> Soldiers: {Soldiers}<space=1em> Food: {Food}";
    }

    private void GetGameObjectAtPosition()
    {
        // only select cells when player already selected a soldier
        string[] layers = State == PlayerState.SelectingSoldier ? new string[] {"Building", "Soldier", "Cell"} : new string[] {"Building", "Soldier"};
        var hit = Utils.GetGameObjectAtLocation(mainCamera.ScreenToWorldPoint(Input.mousePosition), layers);

        if (hit)
        {
            if (State == PlayerState.Moving) return;

            if (hit.collider.CompareTag("Cell"))
            {
                var cell = hit.collider.GetComponent<Cell>();
                start = selectedSoldier.CurrentCell;
                finish = cell;
                var path = FindPath();
                path.Reverse();
                selectedSoldier.StartPath(path);
            }
            else
            {
                var boardElement = hit.collider.GetComponent<BoardElement>();

                if (hit.collider.CompareTag("Building")) // if building
                {
                    if (boardElement.Model.State == BoardElementState.Stationary)
                    {
                        InformationPanel.instance.SetSelectedBoardElement(boardElement);
                        State = PlayerState.SelectingBuilding;
                    }
                }
                else // if soldier
                {
                    selectedSoldier = (SoldierController) boardElement.Controller;
                    selectedSoldier.CurrentCell = Utils.GetGameObjectAtLocation(selectedSoldier.transform.position, "Cell").collider.GetComponent<Cell>();
                    InformationPanel.instance.SetSelectedBoardElement(boardElement);
                    State = PlayerState.SelectingSoldier;
                }
            }
        }
        else
        {
            //InformationPanel.instance.ClearSelectedBoardElement();
            State = PlayerState.Normal;
        }
    }

    private static List<Tile> GetAvailableTiles(Cell[,] map, Tile currentTile, Tile targetTile)
    {
        var possibleTiles = new List<Tile>()
        {
            new Tile {X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1},
            new Tile {X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
            new Tile {X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1},
            new Tile {X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1},
        };

        possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

        var maxX = map.GetLength(1);
        var maxY = map.GetLength(0);

        return possibleTiles
            .Where(tile => tile.X >= 0 && tile.X <= maxX)
            .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
            .Where(tile => GridManager.instance.GetCellByTile(tile).State != CellState.Unavailable)
            .ToList();
    }

    private List<Cell> FindPath()
    {
        var map = GridManager.instance.Map;

        var startTile = start.Tile;
        var finishTile = finish.Tile;

        startTile.SetDistance(finishTile.X, finishTile.Y);

        var activeTiles = new List<Tile>();
        var path = new List<Cell>();
        activeTiles.Add(startTile);
        var visitedTiles = new List<Tile>();

        while (activeTiles.Any())
        {
            var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

            if (checkTile.X == finishTile.X && checkTile.Y == finishTile.Y)
            {
                var tile = checkTile;

                while (true)
                {
                    var cell = GridManager.instance.GetCellByTile(tile);
                    cell.SetState(CellState.Available);
                    path.Add(cell);

                    tile = tile.Parent;
                    if (tile == null)
                    {
                        return path;
                    }
                }
            }

            visitedTiles.Add(checkTile);
            activeTiles.Remove(checkTile);

            var walkableTiles = GetAvailableTiles(map, checkTile, finishTile);

            foreach (var walkableTile in walkableTiles)
            {
                if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    continue;

                if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                {
                    var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                    if (existingTile.CostDistance > checkTile.CostDistance)
                    {
                        activeTiles.Remove(existingTile);
                        activeTiles.Add(walkableTile);
                    }
                }
                else
                {
                    activeTiles.Add(walkableTile);
                }
            }
        }

        Debug.Log("No Path Found!");
        return path;
    }
}