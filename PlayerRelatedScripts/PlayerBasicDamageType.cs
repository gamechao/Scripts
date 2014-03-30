using UnityEngine;
using System.Collections;

public class PlayerBasicDamageType : MonoBehaviour 
{
	public float amount;//the damage amount
	
	
	EnemyStatsBasic enemy;//enemy stats
	
	protected virtual void OnTriggerEnter(Collider other)
	{
		//print (other);
		//check if we are hiting enemy
		if(other.tag == "Enemy")
		{
			enemy = other.GetComponent<EnemyController>().enemyStats;
			//print (enemy + "----" +enemy.enemyHealth );
			enemy.GetDamage(amount);
		}
	}
	
}
