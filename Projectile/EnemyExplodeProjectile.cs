using UnityEngine;
using System.Collections;

public class EnemyExplodeProjectile : EnemyBasicProjectile 
{
	//the explode effect doesnt move
	public override void Move()
	{
		//transform.Translate(Vector3.forward);
	}
	
}
