using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour 
{
	State currentState = null;

	void Update () 
	{
		if(currentState != null)
		{
			currentState.UpdateState();
			//print (currentState);
		}
		else
		{
			print ("no state");
		}
	}
	
	public void SetState(State newState)
	{
		if(currentState != null)
		{
			currentState.ExitState();
		}
		
		currentState = newState;
		
		if(currentState != null)
		{
			currentState.EnterState();
		}
	}
}
