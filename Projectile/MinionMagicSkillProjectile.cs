using UnityEngine;
using System.Collections;

public class MinionMagicSkillProjectile : MinionMagicProjectile 
{
	protected override void Start()
	{
		base.Start();
		amount =50;
	}
	
	void Update ()
	{
		//print (amount);
	}
	
}
