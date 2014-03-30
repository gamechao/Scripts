using UnityEngine;
using System.Collections;

public class EnemyExplodeState : BasicAttackState 
{
	EnemyExplosivePawn enemyController;
	
	public EnemyExplodeState(EnemyExplosivePawn enemy)
	{
		enemyController = enemy;
	}
	
	public override void EnterState()
	{
		
	}
	//still moving the enemy in exploding attack and increase the scale
	public override void UpdateState()
	{
		if(enemyController.TrackProjectile() == false)
		{
			enemyController.StartCoroutine("Attack");
			//Debug.LogWarning("in attack state");
		}
		enemyController.Move();
		//Debug.LogWarning("exploding");
		enemyController.IncreaseScale();
		/*
		if(enemyController.isExploder == true)
		{
			enemyController = enemyController as EnemyExplosivePawn;
			Debug.LogWarning("1111");
			enemyController.Move();
			enemyController.
		}*/
	}

	public override void ExitState()
	{
		//enemyController.canAttack = false;
		enemyController.StopCoroutine("Attack");
	}
}
