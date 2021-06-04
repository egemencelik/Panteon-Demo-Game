using UnityEngine;
public class SoldierModel : BoardElementModel
{
	[Header("Soldier")]
	public int MoveSpeed;

	public float WaitDelay => 3f / MoveSpeed;
}