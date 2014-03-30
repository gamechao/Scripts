using UnityEngine;
using System.Collections;

public class MinionMeleePawn : MinionBasicPawn
{
	//minion unique stats
	MinionBasicStats meleeStats;
	StatsTransfer meleeStatsTransfer = new StatsTransfer();
	private float skillDuration = 5f;
	
	MinionRangeProjectile meleePorjectile;
	
	void Awake()
	{
		summonSourceRequireAmount = 10;
	}
	
	protected override void Start () 
	{
		base.Start();
		//modify the stats
		meleeStats = minionStats;
		meleeStats.level = 3;
		meleeStats.minionAgility = 5;
		meleeStats.minionStrength = 20;
		meleeStats.minionConstitution = 30;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
		damage = meleeStatsTransfer.CalculateDamage(meleeStats.minionStrength,meleeStats.level);
		damageReduce = meleeStatsTransfer.CalculateDamageReducePercentage(meleeStats.minionConstitution);
		dodge = meleeStatsTransfer.CalculateDodge(meleeStats.minionAgility);
		//print ("enemy"+damage+"   "+damageReduce+"   "+dodge);
	}
	
	public override IEnumerator Attack()
	{		
		//spawn the projectile 
		curProjectile = Instantiate(projectile,this.transform.position,this.transform.rotation) as GameObject;
		//set the damage
		curProjectile.GetComponent<BasicProjectile>().SetAmount(damage);
		meleePorjectile = curProjectile.GetComponent<MinionRangeProjectile>();
		meleePorjectile.targetEnemy = currentEnemy;
		yield return new WaitForSeconds(interval);		
	}
	
	public override IEnumerator useSkill ()
	{
		meleeStats.minionStrength = 50;
		//print (meleeStats.minionStrength);
		yield return new WaitForSeconds(skillDuration);
		meleeStats.minionStrength = 20;
		//print (meleeStats.minionStrength);
	}
}
