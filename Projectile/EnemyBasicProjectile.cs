using UnityEngine;
using System.Collections;

public class EnemyBasicProjectile : BasicProjectile 
{
		
	
	protected override void OnTriggerEnter(Collider other)
	{
		switch(other.transform.tag)
		{
			case "Player":
				//if the projectile is no longer enabled then it wont do damage
				if(isEnabled == false)
				{
					return;
				}
				if(Random.value> other.GetComponent<PlayerScript>().dodge)
				{
					//print (other.GetComponent<PlayerScript>().dodge);
					damageReduceMultiplier = other.GetComponent<PlayerScript>().damageReduce;
					curPlayerStats.GetDamage(amount*damageReduceMultiplier);
				}
				//print (curPlayerStats.playerHealth);
				//disable the projectile after damage
				isEnabled = false;
				renderer.enabled = false;
				break;
			case "Shield":
				if(isEnabled == false)
				{
					return;
				}
				curPlayerStats.DecreaseShield(amount);
				isEnabled = false;
				renderer.enabled = false;
				break;
			case "Minion":
				if(isEnabled == false)
				{
					return;
				}
				damageReduceMultiplier = other.GetComponent<MinionBasicPawn>().damageReduce;
				other.GetComponent<MinionBasicPawn>().minionStats.GetDamage(amount*damageReduceMultiplier);
				isEnabled = false;
				renderer.enabled = false;
				break;
		}
	}
}
