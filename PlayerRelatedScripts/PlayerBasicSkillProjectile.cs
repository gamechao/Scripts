using UnityEngine;
using System.Collections;

public class PlayerBasicSkillProjectile : BasicSkill 
{
	public float speed;//move speed

	//move forward
	protected override void doMove()
	{
		transform.Translate(Vector3.forward * speed *Time.deltaTime);	
	}

}
