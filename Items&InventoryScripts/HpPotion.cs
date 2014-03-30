using UnityEngine;
using System.Collections;

public class HpPotion : Item 
{
	public int amount;//the amount potion heals
	
	protected override void Start()
	{
		base.Start();
	}
	
	public override void UseItem ()
	{
		//if current health is lower then max health then we can use potion
		if(curPlayerStats.playerHealth<curPlayerStats.playerMaxHealth)
		{	
			curPlayerStats.AddHealth(amount);
			//subtract the number of stack
			subtractStack();
		}
	}

}
