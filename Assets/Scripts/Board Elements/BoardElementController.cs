using System;
using System.Collections;
using UnityEngine;

public class BoardElementController : BaseBoardElementComponent
{
	// only use 75% of the size to make visuals more correctly
	private static float sizeOfCell => .32f * .75f;
	private float elapsed;
	private float interval => boardElement.Model.ActivityInterval;

	public void SetState(BoardElementState state)
	{
		boardElement.Model.State = state;
	}

	protected virtual void OnCreate()
	{
	}

	public virtual void OnMove()
	{
		
	}

	protected virtual void ElementActivity()
	{
	}

	protected virtual void Update()
	{
		if (boardElement.Model.State == BoardElementState.Stationary)
		{
			DoActivity();
		}
	}

	// runs according to building interval when building is stationary
	private void DoActivity()
	{
		if (interval != 0)
		{
			elapsed += Time.deltaTime;
			if (elapsed >= interval)
			{
				elapsed %= interval;
				ElementActivity();
			}
		}
	}

	// Changes collider size according to the cell sizes on the model and makes element moving
	public void Initialize()
	{
		var model = boardElement.Model;
		SetState(BoardElementState.Moving);
		boardElement.View.SetColliderSize(new Vector2(model.CellSizeX * sizeOfCell, model.CellSizeY * sizeOfCell));
	}
}