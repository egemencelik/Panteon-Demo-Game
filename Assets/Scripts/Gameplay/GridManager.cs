using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	[SerializeField]
	private GameObject gridGO;

	[SerializeField]
	private int rows, cols;

	// Sprite size 32x32 divided by pixels per unit
	private const float SCALE = (float)32 / 100;

	public static GridManager instance;

	public Cell[,] Map;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		Map = new Cell[cols, rows];
		GenerateGrids();
	}

	private void GenerateGrids()
	{
		for (var row = 0; row < rows; row++)
		{
			for (var col = 0; col < cols; col++)
			{
				var grid = Instantiate(gridGO, transform);
				grid.transform.localPosition = new Vector2(col, -row) * SCALE;
				var cell = grid.GetComponent<Cell>();
				cell.Tile = new Tile { X = col, Y = row, Parent = null, Cost = 0 };
				Map[col, row] = cell;
			}
		}
	}

	public Cell GetCellByTile(Tile tile)
	{
		return Map[tile.X, tile.Y];
	}
}