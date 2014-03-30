using UnityEngine;
using System.Collections;

public class PlayerReplicationInfo  
{
	//tuning values
	public int level;
	public int playerHealth = 100;//player health
	public int playerMaxHealth = 100;//player max health
	public int summonSource = 0;//summon source
	public int gold = 0;		//gold
	public int shieldAmount = 0;
	
	//properties
	public int playerStrength = 0;//cause player cant attack
	public int playerAgility = 20;
	public int playerConstitution = 20;
	
	//current health should alway equal or below max health
	public void FixHealth()
	{
		if(playerHealth>playerMaxHealth)
		{
			playerHealth = playerMaxHealth;
		}
	}
	//add certain amount to player health
	public void AddHealth(int num)
	{
		playerHealth += num;
	}
	//add certain amount to summon source
	public void AddSource(int num)
	{
		summonSource += num;
	}
	//add certain amount to gold
	public void AddGold(int num)
	{
		gold += num;
	}
	//substract certain amount to player health
	public void GetDamage(float num)
	{
		playerHealth -= (int)num;
	}
	
	public void AddShield(int num)
	{
		shieldAmount += num;
	}
	
	public void DecreaseShield(int num)
	{
		shieldAmount -= num;
	}
	
	public bool CheckShield()
	{
		if(shieldAmount > 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public void SubtractSummonSource(int num)
	{
		summonSource -= num;
	}
	
	public void ResetShield()
	{
		shieldAmount =0;
	}
}
