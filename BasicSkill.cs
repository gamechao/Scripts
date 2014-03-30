using UnityEngine;
using System.Collections;

public class BasicSkill : MonoBehaviour 
{
	//player information
	public PlayerScript player;
	public PlayerSkill skill;
	public PlayerReplicationInfo playerInfo;
	
	//skill tuning
	public int skillType;
	
	protected virtual void Start()
	{
		//access the player information
		player = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
		skill = FindObjectOfType(typeof(PlayerSkill)) as PlayerSkill;
		playerInfo = player.playerStats;
		//kill the skill object after the timer finish
		StartCoroutine( killSkillObject(skill.skillDuration[skillType]));
	}
	
	protected virtual void Update()
	{
		doMove();
	}
	
	//move 
	protected virtual void doMove()
	{
		transform.position = player.transform.position;
	}
	
	//kill the object
	protected virtual IEnumerator killSkillObject(float time)
	{
		yield return new WaitForSeconds(time);
		//print (time);
		Destroy(gameObject);
	}
}
