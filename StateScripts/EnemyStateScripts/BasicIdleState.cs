using UnityEngine;
using System.Collections;

public class BasicIdleState : State 
{
	EnemyController enemyController;
		
	public BasicIdleState(EnemyController enemy)
	{
		enemyController = enemy;	
	}
	
	public override void EnterState()
	{
		
	}
	//do idle
	public override void UpdateState()
	{
		enemyController.Idle();
	}

	public override void ExitState()
	{
		
	}
}
