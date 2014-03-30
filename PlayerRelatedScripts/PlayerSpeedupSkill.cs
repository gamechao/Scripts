using UnityEngine;
using System.Collections;

public class PlayerSpeedupSkill : BasicSkill 
{
	//tuning
	public int speedModifier;
	
	protected override void Start () 
	{
		base.Start();
		//change speed
		player.speed *= speedModifier;
		StartCoroutine("ResetPlayerSpeed");
	}
	
	IEnumerator ResetPlayerSpeed()
	{
		yield return new WaitForSeconds(skill.skillDuration[skillType]);
		player.speed = 5;
	}
}
