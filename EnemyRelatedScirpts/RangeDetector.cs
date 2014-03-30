using UnityEngine;
using System.Collections;

public class RangeDetector : MonoBehaviour 
{
	//enemy
	EnemyController enemy;
	
	void Start()
	{
		//access enemy
		enemy = gameObject.transform.parent.GetComponent<EnemyController>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		//if the detector collides player or minion go to attack state
		if(other.transform.tag =="Player"||other.transform.tag=="Minion")
		{
			if(enemy.isExploder == false)
			{
				enemy.canAttack = true;
				
				enemy.GotoAttackState();	
				//print ("finish attack");
			}
			//if it's a exploder then goto explode state
			else
			{
				enemy.canAttack = true;
				//enemy.GotoExplodeState();
				enemy.GetComponent<EnemyExplosivePawn>().GotoExplodeState();
			}
		}	
	}
}
