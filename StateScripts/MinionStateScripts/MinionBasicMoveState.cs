using UnityEngine;
using System.Collections;

public class MinionBasicMoveState : State 
{

	MinionBasicPawn minionController;
	
	public MinionBasicMoveState(MinionBasicPawn minion)
	{
		//access controller
		minionController = minion;
	}
	
	
	public override void EnterState()
	{

	}
	//move enemy
	public override void UpdateState()
	{
		Debug.Log("in move state");
		minionController.Move();
	}
	
	public override void ExitState()
	{

	}
}
