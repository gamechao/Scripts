using UnityEngine;
using System.Collections;

public class MinionMagicProjectile : MinionBasicProjectile 
{
	public GameObject targetEnemy;
	
	// Update is called once per frame
	void Update () 
	{
		if(targetEnemy == null)
		{
			return;
		}
		transform.LookAt(targetEnemy.transform);
		Move();
	}
}
