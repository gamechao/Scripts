using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplayMinionSkills : MonoBehaviour 
{
	//tuning position
	public float ButtonPosX = 10;
	public float ButtonPosY = 20;
	public int ButtonWidth = 40;
	public int ButtonHeight = 40;
	public int xModifier = 50;
	public int yModifier = 10;
	public int labelXModifier = 17;
	
	//icons
	public List<Texture2D> minionSkillIcon;	
	public List<Texture2D> minionSkillCoolDownIcon;

	
	//varibles for function
	public string minionSkillButtonName;
	public List<GameObject> skillPrefab;
	
	//skill specific varibles
	public bool minionSkillButtons;//buttons for skills
	public List<float> skillTimer;//count down timers for skills
	public List<float> skillDefaultTimer;
	public List<string> skillDescription;
	
	
	private bool[] hasMinion = new bool[4];
	
	//if skills are enabled by equiping specific equipments
	public bool[] enableMinionSkill = new bool[4];
	
	//if skills are in cool down
	public bool[] MinionSkillCoolDown = new bool[4];
	
	DisplayMinion minionsInArray;
	
	int toggleFlag = 0;
	
	// Use this for initialization
	void Start () 
	{
		minionsInArray = GameObject.Find("GameMaster").GetComponent<DisplayMinion>();
		checkMinions();
		InitSkill();
	}
	

	void Update () 
	{
		checkMinions();
		if(Input.GetButtonDown(minionSkillButtonName)|| minionSkillButtons)
		{
			if(enableMinionSkill[toggleFlag]==true)
			{
				//print (toggleFlag);
				CastMinionSkill(toggleFlag);
				enableMinionSkill[toggleFlag]= false;
				MinionSkillCoolDown[toggleFlag]=true;
			}
		}
		
		for (int l = 0; l<4;l++)
		{
			if(MinionSkillCoolDown[l] == true)
			{
				RunCoolDown(l);
			}
		}
		
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			if(toggleFlag<3)
			{
				toggleFlag++;
			}
			else
			{
				toggleFlag = 0;
			}
		}
	}
	
	//initialize skills
	private void InitSkill()
	{
		for(int l = 0;l<4;l++)
		{
			enableMinionSkill[l] = true;
		}
	}
	
	//use this function to check if we have summoned minions 
	private void checkMinions()
	{
		for(int i= 0;i<4;i++)
		{
			if(minionsInArray.minions[i].Count>0)
			{
				hasMinion[i] = true;
			}
			else
			{
				hasMinion[i] = false;
			}
		}
	}
	
	
	public void CastMinionSkill(int type)
	{
		//Instantiate(skillPrefab[type],transform.position,Quaternion.identity );
		//cast minionskill
		for(int i = 0; i<minionsInArray.minions[type].Count;i++)
		{
			switch(type)
			{
				case 0:
					minionsInArray.minions[type][i].GetComponent<MinionMeleePawn>().StartCoroutine("useSkill");
					break;
				case 1:
					minionsInArray.minions[type][i].GetComponent<MinionMagicPawn>().StartCoroutine("useSkill");
					break;
				case 2:
					minionsInArray.minions[type][i].GetComponent<MinionRangePawn>().StartCoroutine("useSkill");
					break;
				case 3:
					minionsInArray.minions[type][i].GetComponent<MinionHealPawn>().StartCoroutine("useSkill");
					break;
				default:
					Debug.LogWarning("no minions selected!");
					break;
			}
		}
		
	}

	//use timer to trace the cool down of skills and updating the cool down timer
	public void RunCoolDown(int type)
	{
		if(skillTimer[type]>0)
		{
			skillTimer[type] -= Time.deltaTime;
		}
		else
		{
			skillTimer[type]=0;
			MinionSkillCoolDown[type]=false;
			enableMinionSkill[toggleFlag]=true;
			ResetTimer(type);
		}
		
	}
	
	//reset the timer
	private void ResetTimer(int type)
	{
		skillTimer[type] = skillDefaultTimer[type];
	}
	
	//GUI function to show the skill buttons
	public void OnGUI()
	{
		//creat a group for the skill buttons
		GUI.BeginGroup(new Rect(100,Screen.height -150,300,80));
		//if skills are enabled then show the buttons
		//int toggleFlag = 0;
	
		if(hasMinion[toggleFlag]==true)
		{
			minionSkillButtons = GUI.Button(new Rect(ButtonPosX,ButtonPosY,ButtonWidth,ButtonHeight),new GUIContent(minionSkillIcon[toggleFlag],skillDescription[toggleFlag]));
			GUI.Label(new Rect(ButtonPosX+40,ButtonPosY-10,200,60),GUI.tooltip);
		}
		else
		{
			minionSkillButtons = false;
		}

		//set the cooldown timer label font color to red
		GUI.contentColor = Color.red;
		//if the skills are in cool down then show the cooldown icon and label 

		
		if(MinionSkillCoolDown[toggleFlag]==true)
		{
			//if(minionSkillButtons)
			//{
				GUI.DrawTexture(new Rect(ButtonPosX,ButtonPosY,ButtonWidth,ButtonHeight),minionSkillCoolDownIcon[toggleFlag]);
				GUI.Label(new Rect(ButtonPosX+labelXModifier,ButtonPosY+yModifier,ButtonWidth,ButtonHeight),""+Mathf.FloorToInt( skillTimer[toggleFlag]));
			//}
		}
		GUI.EndGroup();
	}
}
