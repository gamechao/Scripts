using UnityEngine;
using System.Collections;

public class MinionBasicStats 
{
	//values
	public int level;
	public float minionHealth = 100;//current health
	public float minionMaxHealth = 100;//max health
	
	//properties
	public int minionStrength;
	public int minionAgility;
	public int minionConstitution;
	
	//regen function  amount will base on the minion type
	public void RegenHealth(float num)
	{
		if(minionHealth<minionMaxHealth)
		{
			minionHealth += num;
		}
		else
		{
			minionHealth = minionMaxHealth;
		}
	}
	
	//receive damage modify hp
	public void GetDamage(float num)
	{
		minionHealth -= num;
	}
}
