using UnityEngine;
using System.Collections;

public class PlayerGUIScirpt : MonoBehaviour 
{
	
	public Texture2D HpBarTexture;//Hp bar texuture
	
	//position tuning value
	public int groupXPos;
	public int groupYPos;
	public int groupWidth;
	public int groupHeight;
	public int labelXPos;
	public int labelYPos;
	public int labelWidth;
	public int labelHeight;
	public int sourceLabelXPos;
	public int sourceLabelYPos;
	public int sourceLabelWidth;
	public int sourceLabelHeight;
	public int barXPos;
	public int barYPos;
	public int barHeight;
	private float hpBarLength ;//the length is managed by code so we dont want to change it in editor	
	
	//health related varibles
	private float healthPointonGUI;//current health
	private float healthPointMaxonGUI;//max health
	private float percentOfHP ;//curent health percentage 
	
	
	//get player information
	public PlayerReplicationInfo playerStatsGUI;
	public PlayerScript player;
	// Use this for initialization
	void Start () 
	{
		//get player information  (assign player stats here will get a null reference error)
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
		if(player.playerStats != null)
		{
			playerStatsGUI = player.playerStats;
		}
		else
		{
			print ("cant find player states");
		}
	}
	
	void Update()
	{
		//assign here to fix the error above
		if(player.playerStats !=null)
		{
			playerStatsGUI = player.playerStats;
		}
		//print (hpBarLength + "    "+ healthPointMaxonGUI+"   "+healthPointonGUI+"  "+hpBarLength);
		UpdateHP();//always update the current health
	}
	
	void OnGUI () 
	{
		//creat a group for player information
		GUI.BeginGroup(new Rect(groupXPos,groupYPos,groupWidth,groupHeight));
		//hp label
		GUI.Label(new Rect(labelXPos,labelYPos,labelWidth,labelHeight),"HP");
	    if (healthPointonGUI > 0)
	    {
			//hp bar
	        GUI.DrawTexture(new Rect(barXPos, barYPos, hpBarLength, barHeight), HpBarTexture);
	    }
		//resource label
		GUI.Label(new Rect(sourceLabelXPos,sourceLabelYPos,sourceLabelWidth,sourceLabelHeight),"Source:"+playerStatsGUI.summonSource);
		GUI.EndGroup();
	}
 	
	void UpdateHP()
	{	
		//get information from player stats
		healthPointonGUI = playerStatsGUI.playerHealth;
		healthPointMaxonGUI = playerStatsGUI.playerMaxHealth;
		//divide to get the percentage
		percentOfHP = healthPointonGUI/healthPointMaxonGUI;
		//modify percentage to length
		hpBarLength = percentOfHP*100;
	}
}
