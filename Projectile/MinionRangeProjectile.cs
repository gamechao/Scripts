using UnityEngine;
using System.Collections;

public class MinionRangeProjectile : MinionBasicProjectile 
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
