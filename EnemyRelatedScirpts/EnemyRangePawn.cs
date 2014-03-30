using UnityEngine;
using System.Collections;

public class EnemyRangePawn : EnemyController 
{
	EnemyStatsBasic rangeStats;//unique stats
	StatsTransfer rangeStatsTransfer = new StatsTransfer();//for stats transfer
	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		//get the basic stats and reset them
		rangeStats = enemyStats;
		rangeStats.level = 5;
		rangeStats.enemyAgility = 5;
		rangeStats.enemyStrength = 25;
		rangeStats.enemyConstitution = 0;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
		//do stats transfer
		damage = rangeStatsTransfer.CalculateDamage(rangeStats.enemyStrength,rangeStats.level);
		damageReduce = rangeStatsTransfer.CalculateDamageReducePercentage(rangeStats.enemyConstitution);
		dodge = rangeStatsTransfer.CalculateDodge(rangeStats.enemyAgility);
	}
}
