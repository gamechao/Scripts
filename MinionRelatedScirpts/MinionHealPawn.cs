using UnityEngine;
using System.Collections;

public class MinionHealPawn : MinionBasicPawn 
{
	//minion unique stats
	MinionBasicStats healStats;
	StatsTransfer healStatsTransfer = new StatsTransfer();
	private float skillDuration = 5f;
	
	void Awake()
	{
		summonSourceRequireAmount = 30;
	}
	
	protected override void Start () 
	{
		base.Start();
		//modify the stats
		healStats = minionStats;
		healStats.level = 0;
		healStats.minionAgility = 30;
		healStats.minionStrength = 0;
		healStats.minionConstitution = 5;
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
		damage = healStatsTransfer.CalculateDamage(healStats.minionStrength,healStats.level);
		damageReduce = healStatsTransfer.CalculateDamageReducePercentage(healStats.minionConstitution);
		dodge = healStatsTransfer.CalculateDodge(healStats.minionAgility);
	}
	
	public override IEnumerator Attack()
	{		
		yield return new WaitForSeconds(interval);
	}
	
	private void CastHeal()
	{
		DisplayMinion minions;
		minions = GameObject.Find("GameMaster").GetComponent<DisplayMinion>();
		//looping through all our summoned minions
		for(int i = 0; i<minions.meleeMinions.Count;i++)
		{
			if(minions.meleeMinions[i]==null)
			{
				return;
			}
			if(minions.meleeMinions[i].GetComponent<MinionMeleePawn>().minionStats.minionHealth < minions.meleeMinions[i].GetComponent<MinionMeleePawn>().minionStats.minionMaxHealth)
			{
				minions.meleeMinions[i].GetComponent<MinionMeleePawn>().minionStats.RegenHealth(5);
			}
		}
		for(int k = 0; k<minions.magicMinions.Count;k++)
		{
			if(minions.magicMinions[k]==null)
			{
				return;
			}
			//print (minions.magicMinions[k]);
			if(minions.magicMinions[k].GetComponent<MinionMagicPawn>().minionStats.minionHealth < minions.magicMinions[k].GetComponent<MinionMagicPawn>().minionStats.minionMaxHealth)
			{
				minions.magicMinions[k].GetComponent<MinionMagicPawn>().minionStats.RegenHealth(5);
			}
		}
		for(int l = 0; l<minions.rangeMinions.Count;l++)
		{
			if(minions.rangeMinions[l]==null)
			{
				return ;
			}
			//print (minions.rangeMinions[l]);
			if(minions.rangeMinions[l].GetComponent<MinionRangePawn>().minionStats.minionHealth < minions.rangeMinions[l].GetComponent<MinionRangePawn>().minionStats.minionMaxHealth)
			{
				minions.rangeMinions[l].GetComponent<MinionRangePawn>().minionStats.RegenHealth(5);
			}
		}
		for(int n = 0; n<minions.healMinions.Count;n++)
		{
			if(minions.healMinions[n].GetComponent<MinionHealPawn>().minionStats.minionHealth < minions.healMinions[n].GetComponent<MinionHealPawn>().minionStats.minionMaxHealth)
			{
				minions.healMinions[n].GetComponent<MinionHealPawn>().minionStats.RegenHealth(5);
			}
		}
	}
	
	public override IEnumerator useSkill ()
	{
		CastHeal();
		yield return new WaitForSeconds(skillDuration);
		//print ("use Heal skill");
	}
}
