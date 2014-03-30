using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplayMinion : MonoBehaviour 
{
	
	public bool displayMinion = false;//if we need to display the minion
	public Formation form;//get formation
	public bool isEnabled = false;
	
	//minions
	public List<GameObject>[] minions = new List<GameObject>[4];
	public List<GameObject> meleeMinions;
	public List<GameObject> rangeMinions;
	public List<GameObject> magicMinions;
	public List<GameObject> healMinions;
	
	//raycasting
	public Ray myMinionRay;// ray use to tracing the spawner
	public RaycastHit[] myMinionHit;// ray hit object
	
	//tuning
	public GameObject[] minionPrefabs;//array for all the minions we have then we can use it to toggle the minions we want to spawn
	public Texture2D minionIcon;//the icon of minion button
	public int minionType;//minion type
	
	//minion button tuning values in editor
	public int buttonXPos;
	public int buttonYPos;
	public int buttonWidth;
	public int buttonHeight;
	
	//sub buttons tuning values in editor
	public int subStartXPos;
	public int subStartYPos;
	public int xPosModifier;
	public int subButtonWidth;
	public int subButtonHeight;
	
	//player
	private GameObject player;
	private PlayerReplicationInfo playerStatsForSummon;
	
	
	
	private bool isSummon = false;//check if we can summon a minion
	// Use this for initialization
	void Start () 
	{
		//access the formation
		form = FindObjectOfType(typeof(Formation))as Formation;
		
		//access the player
		player = GameObject.Find("Player");
		playerStatsForSummon = player.GetComponent<PlayerScript>().playerStats;
		
		//initiate the minion array
		minions[0] = meleeMinions; 
		minions[1] = magicMinions; 
		minions[2] = rangeMinions; 
		minions[3] = healMinions; 
	}
	
	
	void Update()
	{
		SummonMinion(minionType);
		
		//print (myHit.transform.tag);
		//print (Input.mousePosition);
	}
	
	public void OnGUI()//display
	{
		if(minionIcon != null && isEnabled == true)//if we have minion icon then create button
		{
			bool minionButton = GUI.Button(new Rect(Screen.width-buttonXPos,buttonYPos,buttonWidth,buttonHeight),new GUIContent(minionIcon,"Summon Minions"));
			GUI.Label(new Rect(Screen.width-buttonXPos-70,buttonYPos+40,150,20),GUI.tooltip);
			GUI.tooltip = "";
			//if(GUI.Button(new Rect(Screen.width-40,180,40,40),minionIcon))
			if(minionButton)//if button is clicked toggle display minion
			{
				//print ("make button");
				if(displayMinion)
				{
					displayMinion = false;
					form.HideFormation();//if the summon window is off hide the formation
				}
				else
				{
					displayMinion = true;
					form.ShowFormation();//if the summon window is on show the formation
				}
			}
		}
		
		if(displayMinion)//create the minion type buttons and use them to change minion
		{
			bool minionMeleeButton = GUI.Button (new Rect (Screen.width-subStartXPos, subStartYPos, subButtonWidth,subButtonHeight),new GUIContent("Melee\nMinion","Left click to choose type and click on spawner to summon"));
			bool minionMagicButton = GUI.Button (new Rect (Screen.width-subStartXPos+xPosModifier, subStartYPos, subButtonWidth, subButtonHeight),new GUIContent( "Magic\nMinion","Left click to choose type and click on spawner to summon"));
			bool minionRangeButton = GUI.Button (new Rect (Screen.width-subStartXPos+(xPosModifier*2), subStartYPos, subButtonWidth, subButtonHeight), new GUIContent("Range\nMinion","Left click to choose type and click on spawner to summon"));
			bool minionHealButton = GUI.Button (new Rect (Screen.width-subStartXPos+(xPosModifier*3), subStartYPos, subButtonWidth, subButtonHeight), new GUIContent("Heal\nMinion","Left click to choose type and click on spawner to summon"));
			GUI.Label(new Rect(Screen.width-buttonXPos-300,buttonYPos+40,350,20),GUI.tooltip);
					
			if(minionMeleeButton)//button of minion, change the type while click and make player able to summon
			{
				minionType = 0;
				isSummon =true;
			}
			if(minionMagicButton)
			{
				minionType = 1;
				isSummon =true;
			}
			if(minionRangeButton)
			{
				minionType = 2;
				isSummon =true;
			}
			if(minionHealButton)
			{
				minionType = 3;
				isSummon =true;
			}
			
		}
	}
	

	public void SummonMinion(int type)//summon the minions with certain type
	{
		myMinionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		myMinionHit =  Physics.RaycastAll(myMinionRay, 1000);
		int i = 0;
		while(i<myMinionHit.Length)
		{
			RaycastHit hit = myMinionHit[i];
			if(hit.transform.tag =="spawner" )
			if(isSummon == true)//if we can summon
			{
				if(hit.transform.tag=="spawner")//if our ray hits the spawner
				{
					if(Input.GetKeyDown(KeyCode.Mouse0))//if we left click on it
					{
						Transform owner = hit.transform;
						//print (owner);
						//print ("summon");
						int summonRequirement = minionPrefabs[type].GetComponent<MinionBasicPawn>().summonSourceRequireAmount;
						//print (summonRequirement);
						if(playerStatsForSummon.summonSource<summonRequirement)
						{
							print ("not enough summon source");
							return;
						}
						
						if(owner.transform.childCount == 0)
						{
							GameObject minion = Instantiate(minionPrefabs[type],owner.position,owner.rotation) as GameObject;
							minion.transform.position = owner.transform.position;
							//for fix
							minion.GetComponent<MinionBasicPawn>().spawner = owner;
							minions[type].Add(minion);
							playerStatsForSummon.SubtractSummonSource(summonRequirement);
							isSummon=false;
						}
					}
				}
			}
			i++;
		}
	}
}
