using UnityEngine;
using System.Collections;

public abstract class State
{
	public abstract void EnterState();
	public abstract void UpdateState();
	public abstract void ExitState();
}
