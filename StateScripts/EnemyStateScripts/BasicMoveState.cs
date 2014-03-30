using UnityEngine;
using System.Collections;

public class BasicMoveState : State 
{
	EnemyController enemyController;
	
	public BasicMoveState(EnemyController enemy)
	{
		enemyController = enemy;
	}

	
	public override void EnterState()
	{
		
	}
	//move enemy
	public override void UpdateState()
	{
		enemyController.Move();
	}

	public override void ExitState()
	{
		
	}
}
