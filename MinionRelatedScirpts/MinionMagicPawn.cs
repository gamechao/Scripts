using UnityEngine;
using System.Collections;

public class MinionMagicPawn : MinionBasicPawn 
{
	//skill prefab
	public GameObject skillPrefab;
	
	//minion unique stats
	MinionBasicStats magicStats;
	StatsTransfer magicStatsTransfer = new StatsTransfer();
	
	MinionMagicProjectile magicPorjectile;
	
	void Awake()
	{
		summonSourceRequireAmount = 25;
	}
	
	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		//modify the stats
		magicStats = minionStats;
		magicStats.level = 3;
		magicStats.minionAgility = 5;
		magicStats.minionStrength = 20;
		magicStats.minionConstitution = 0;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
		damage = magicStatsTransfer.CalculateDamage(magicStats.minionStrength,magicStats.level);
		damageReduce = magicStatsTransfer.CalculateDamageReducePercentage(magicStats.minionConstitution);
		dodge = magicStatsTransfer.CalculateDodge(magicStats.minionAgility);
		//print ("enemy"+damage+"   "+damageReduce+"   "+dodge);
	}
	
	public override IEnumerator Attack()
	{		
		//spawn the projectile 
		curProjectile = Instantiate(projectile,this.transform.position,this.transform.rotation) as GameObject;
		//set the damage
		curProjectile.GetComponent<BasicProjectile>().SetAmount(damage);
		magicPorjectile = curProjectile.GetComponent<MinionMagicProjectile>();
		magicPorjectile.targetEnemy = currentEnemy;
		yield return new WaitForSeconds(interval);
	}
	
	public override IEnumerator useSkill ()
	{
		Instantiate(skillPrefab,this.transform.position,Quaternion.identity);
		return null;
		//print ("use Magic skill");
	}
}
