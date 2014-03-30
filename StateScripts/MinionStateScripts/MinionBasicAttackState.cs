using UnityEngine;
using System.Collections;

public class MinionBasicAttackState : State 
{
	//minion controller
	MinionBasicPawn minionController;
	
	public MinionBasicAttackState()
	{
		
	}
	
	public MinionBasicAttackState(MinionBasicPawn minion)
	{
		//access controller
		minionController = minion;
	}
	
	public override void EnterState()
	{
		
	}
	
	//if the projectile is null then we can attack again
	public override void UpdateState()
	{
		if(minionController.TrackProjectile() == false)
		{
			minionController.StartCoroutine("Attack");
			//Debug.LogWarning("in attack state");
		}
		/*
		if(minionController.isExploder == true)
		{
			minionController = minionController as minionExplosivePawn;
			Debug.LogWarning("1111");
			minionController.Move();
			minionController.
		}*/
	}
	//stop attack while exiting
	public override void ExitState()
	{
		//minionController.canAttack = false;
		minionController.StopCoroutine("Attack");
	}
}
