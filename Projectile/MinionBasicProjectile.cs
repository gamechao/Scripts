using UnityEngine;
using System.Collections;

public class MinionBasicProjectile : BasicProjectile
{
	//calculate how the projectile while colliding
	protected override void OnTriggerEnter(Collider other)
	{
		switch(other.transform.tag)
		{

			case "Enemy":		
				//if no longer enabled do nothing
				if(isEnabled == false)
				{
					return;
				}
				//calculate if the projectile hits
				if(Random.value > other.GetComponent<EnemyController>().dodge)
				{
					//print (amount);
					damageReduceMultiplier = other.GetComponent<EnemyController>().damageReduce;
					other.GetComponent<EnemyController>().enemyStats.GetDamage(amount*damageReduceMultiplier);
				}
				isEnabled = false;
				renderer.enabled = false;
				break;
			case "Exploder":
				if(isEnabled == false)
				{
					return;
				}
				if(Random.value > other.GetComponent<EnemyController>().dodge)
				{
					damageReduceMultiplier = other.GetComponent<EnemyController>().damageReduce;
					other.GetComponent<EnemyController>().enemyStats.GetDamage(amount*damageReduceMultiplier);
				}
				isEnabled = false;
				renderer.enabled = false;
				break;
		}
	}
}
