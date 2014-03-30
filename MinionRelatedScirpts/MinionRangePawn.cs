using UnityEngine;
using System.Collections;

public class MinionRangePawn : MinionBasicPawn
{	
	//minion unique stats
	MinionBasicStats rangeStats;
	StatsTransfer rangeStatsTransfer = new StatsTransfer();
	private float skillDuration = 5f;
	
	MinionRangeProjectile rangePorjectile;
	
	void Awake()
	{
		summonSourceRequireAmount = 15;
	}
	
	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		//modify the stats
		rangeStats = minionStats;
		rangeStats.level = 5;
		rangeStats.minionAgility = 30;
		rangeStats.minionStrength = 10;
		rangeStats.minionConstitution = 5;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
		damage = rangeStatsTransfer.CalculateDamage(rangeStats.minionStrength,rangeStats.level);
		damageReduce = rangeStatsTransfer.CalculateDamageReducePercentage(rangeStats.minionConstitution);
		dodge = rangeStatsTransfer.CalculateDodge(rangeStats.minionAgility);
	}
	
	public override IEnumerator Attack()
	{		
		//spawn the projectile 
		curProjectile = Instantiate(projectile,this.transform.position,this.transform.rotation) as GameObject;
		//set the damage
		curProjectile.GetComponent<BasicProjectile>().SetAmount(damage);
		rangePorjectile = curProjectile.GetComponent<MinionRangeProjectile>();
		rangePorjectile.targetEnemy = currentEnemy;
		yield return new WaitForSeconds(interval);
	}
	public override IEnumerator useSkill ()
	{
		rangeStats.minionAgility = 70;
		yield return new WaitForSeconds(skillDuration);
		rangeStats.minionAgility = 30;
		//print ("use Range skill");
	}
}
