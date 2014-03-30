using UnityEngine;
using System.Collections;

public class EnemyRangeProjectile : EnemyBasicProjectile 
{
	//public float height;   further use 
	
	// Update is called once per frame
	void Update () 
	{
		Move ();
	}
	
	//move it forward    maybe change the movement later
	public override void Move()
	{
		transform.Translate(Vector3.forward *speed *Time.deltaTime);
		//rigidbody.AddForce (0,height,0);
	}
}
