using UnityEngine;
using System.Collections;

public class StatsTransfer
{
	public int CalculateDamage(int strength,int level)
	{
		int damage = (level*5)+strength;
		return damage;
	}
	
	public float CalculateDodge(int agility)
	{
		float dodge = (float)agility/(float)100;
		return dodge;
	}
	
	public float CalculateDamageReducePercentage(int constitution)
	{
		float reduceAmount = (float)constitution/(float)100;
		return reduceAmount;
	}
}
