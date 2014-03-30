using UnityEngine;
using System.Collections;

public class EnemyExplosivePawn : EnemyController 
{
	//values
	public float explodeDelay;//the exploding delay	
	public float speedMultiplier;//speed modifier
	public Vector3 scaleMutiplier;//scale modifier
	
	
	//stats
	EnemyStatsBasic exploderStats;//unique stats
	StatsTransfer exploderStatsTransfer = new StatsTransfer();//for stats transfer
	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		//get the basic stats and reset them
		exploderStats = enemyStats;
		exploderStats.level = 0;
		exploderStats.enemyAgility = 0;
		exploderStats.enemyStrength = 50;
		exploderStats.enemyConstitution = 0;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
		//do stats transfer
		damage = exploderStatsTransfer.CalculateDamage(exploderStats.enemyStrength,exploderStats.level);
		damageReduce = exploderStatsTransfer.CalculateDamageReducePercentage(exploderStats.enemyConstitution);
		dodge = exploderStatsTransfer.CalculateDodge(exploderStats.enemyAgility);
	}
	
	//change to explode state
	public void GotoExplodeState()
	{
		stateMachine.SetState(new EnemyExplodeState(this));
	}
	
	//unique attack function for exploder
	public override IEnumerator Attack()
	{
		isExploder =true;
		//modify the speed
		speed += speedMultiplier;
		//print ("damage"+damage);
		yield return new WaitForSeconds (explodeDelay);
		Destroy(gameObject);
	}
	
	//update scale
	public void IncreaseScale()
	{
		this.transform.localScale += scaleMutiplier;
		if(this.transform.localScale.x > 2)
		{
			curProjectile = Instantiate(projectile,this.transform.position,this.transform.rotation) as GameObject;
			curProjectile.GetComponent<BasicProjectile>().SetAmount(damage);
			Destroy(gameObject);
		}
	}

}
