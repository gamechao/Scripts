using UnityEngine;
using System.Collections;

public class MinionRangeDetector : MonoBehaviour 
{
	//minion
	MinionBasicPawn minion;
	
	void Start()
	{
		//access enemy
		minion = gameObject.transform.parent.GetComponent<MinionBasicPawn>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		//print (other);
		//if the detector collides player or minion go to attack state
		if(other.transform.tag =="Enemy"||other.transform.tag=="Exploder")
		{
			minion.currentEnemy = other.gameObject;
			minion.canAttack = true;
			minion.GotoAttackState();
		}	
	}
	
	void OnTriggerStay(Collider other)
	{
		//print (other);
		//if the detector collides player or minion go to attack state
		if(other.transform.tag =="Enemy"||other.transform.tag=="Exploder")
		{
			minion.currentEnemy = other.gameObject;
			minion.canAttack = true;
			minion.GotoAttackState();
		}
		else
		{
			minion.currentEnemy = null;
		}
	}
}
