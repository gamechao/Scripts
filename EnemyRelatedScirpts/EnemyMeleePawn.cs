using UnityEngine;
using System.Collections;

public class EnemyMeleePawn : EnemyController 
{
	EnemyStatsBasic meleeStats;//unique stats
	StatsTransfer meleeStatsTransfer = new StatsTransfer();//for stats transfer
	
	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		//get the basic stats and reset them
		meleeStats = enemyStats;
		meleeStats.level = 1;
		meleeStats.enemyAgility = 0;
		meleeStats.enemyStrength = 15;
		meleeStats.enemyConstitution = 20;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
		//do stats transfer
		damage = meleeStatsTransfer.CalculateDamage(meleeStats.enemyStrength,meleeStats.level);
		damageReduce = meleeStatsTransfer.CalculateDamageReducePercentage(meleeStats.enemyConstitution);
		dodge = meleeStatsTransfer.CalculateDodge(meleeStats.enemyAgility);
		//print ("enemy"+damage+"   "+damageReduce+"   "+dodge);
	}
}
