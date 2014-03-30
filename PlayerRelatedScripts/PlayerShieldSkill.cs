using UnityEngine;
using System.Collections;

public class PlayerShieldSkill : BasicSkill 
{
	//tuning
	public int amount;//the amount adding to shield
	
	protected override void Start () 
	{
		base.Start();
		//add shield value to player
		playerInfo.AddShield(amount);
	}
	
	protected override void Update()
	{
		base.Update();
		//once the shield amount goes under 0 destroy the skill object
		if(playerInfo.CheckShield()==false)
		{
			Destroy(gameObject);
			playerInfo.ResetShield();
		}
	}
}
