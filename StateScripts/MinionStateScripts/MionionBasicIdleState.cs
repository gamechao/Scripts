using UnityEngine;
using System.Collections;

public class MionionBasicIdleState : State 
{

	MinionBasicPawn minionController;
		
	public MionionBasicIdleState(MinionBasicPawn minion)
	{
		minionController = minion;	
	}
	
	public override void EnterState()
	{
		
	}
	//do idle
	public override void UpdateState()
	{
		minionController.Idle();
	}

	public override void ExitState()
	{
		
	}
}
