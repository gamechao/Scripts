using UnityEngine;
using System.Collections;

public class BasicAttackState : State 
{
	//enemy controller
	EnemyController enemyController;
	
	public BasicAttackState()
	{
		
	}
	
	public BasicAttackState(EnemyController enemy)
	{
		//access controller
		enemyController = enemy;
	}
	
	public override void EnterState()
	{
		
	}
	
	//if the projectile is null then we can attack again
	public override void UpdateState()
	{
		enemyController.LookAtPlayer();
		if(enemyController.TrackProjectile() == false)
		{
			enemyController.StartCoroutine("Attack");
			//Debug.LogWarning("in attack state");
		}
		/*
		if(enemyController.isExploder == true)
		{
			enemyController = enemyController as EnemyExplosivePawn;
			Debug.LogWarning("1111");
			enemyController.Move();
			enemyController.
		}*/
	}
	//stop attack while exiting
	public override void ExitState()
	{
		//enemyController.canAttack = false;
		enemyController.StopCoroutine("Attack");
	}
}
