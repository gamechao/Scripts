using UnityEngine;
using System.Collections;

public class EnemyStatsBasic
{
	//values
	public int level;
	public float enemyHealth = 100;//current health
	public float enemyMaxHealth = 100;//max health
	
	//properties
	public int enemyStrength;
	public int enemyAgility;
	public int enemyConstitution;
	
	//regen function for further use (if lose track of player while attack go back and regen)
	public void RegenHealth(float num)
	{
		if(enemyHealth<enemyMaxHealth)
		{
			enemyHealth += num;
		}
		else
		{
			enemyHealth = enemyMaxHealth;
		}
	}
	
	//receive damage modify hp
	public void GetDamage(float num)
	{
		enemyHealth -= num;
	}
}
