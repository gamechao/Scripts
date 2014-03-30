using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSkill : MonoBehaviour 
{
	//varibles for display
	public Camera viewCamera;
	//public string skillName;
	
	//tuning position
	public float ButtonPosX = 10;
	public float ButtonPosY = 20;
	public int ButtonWidth = 40;
	public int ButtonHeight = 40;
	public int xModifier = 50;
	public int yModifier = 10;
	public int labelXModifier = 17;
	
	//icons
	public List<Texture2D> skillIcon;	
	public List<Texture2D> skillCoolDownIcon;
	//public string description;
	
	//varibles for function
	public List<string> buttonName;
	public List<GameObject> affectAreaPrefab;
	public List<GameObject> skillPrefab;
	
	
	//varibles for cast position and cast area
	private bool isCasting =false;
	private Vector3 castPosition;
	private GameObject affectArea;
	private Vector3 affectAreaPosition;
	private Ray myRay;
	private RaycastHit myHit;
	
	//skill specific varibles
	public List<bool> skillButtons;//buttons for skills
	public List<float> skillTimer;//count down timers for skills
	public List<float> skillDefaultTimer;
	public List<float> skillDuration;
	
	//if skills are enabled by equiping specific equipments
	public bool enableSkill1;
	public bool enableSkill2;
	public bool enableSkill3;
	
	//if skills are in cool down
	private bool skill1CoolDown;
	private bool skill2CoolDown;
	private bool skill3CoolDown;
	
	
	// Use this for initialization
	void Start () 
	{
		//init the skills
		enableSkill1 = false;
		enableSkill2 = false;
		enableSkill3 = false;
	}
	

	void Update () 
	{
		//if we clicked the skill buttons or click the keyboard buttons for skills
		//(keyboard "1","2","3"  for skill1,2,3)
		if(Input.GetButtonDown(buttonName[0])|| skillButtons[0])
		{
			InitSkill("Shock Wave",0);
			skill1CoolDown = true;
		}
		else if(Input.GetButtonDown(buttonName[1])|| skillButtons[1])
		{
			InitSkill("Bless of Wind Spirit",1);
			skill2CoolDown=true;
		}
		else if(Input.GetButtonDown(buttonName[2])|| skillButtons[2])
		{
			InitSkill("Holy Shield",2);
			skill3CoolDown=true;
		}
		
		//update the cast position
		UpdateAreaPosition();
		//print(affectAreaPosition);
		
		//if we are casting skill then set skill into cooldown and cast it then destroy affectarea object
		if(isCasting == true)
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				skill1CoolDown=true;
				CastSkill("Shock Wave",0);
				isCasting =false;
				Destroy(affectArea);
			}
				
		}
		
		//run cool down if skills are in cool down
		if(skill1CoolDown == true)
		{
			RunCoolDown(0);
		}
		if(skill2CoolDown == true)
		{
			RunCoolDown(1);
		}
		if(skill3CoolDown == true)
		{
			RunCoolDown(2);
		}
	}
	
	//if the skill needs a cast position then make it goto pending state
	//if the skill cast directly on player then cast it
	public void InitSkill(string sName,int type)
	{
		//check if we clicked the skillbuttons of type the keyboard
		if(Input.GetButtonDown(buttonName[type])|| skillButtons[type])
		{
			if(type == 0)
			{
				//if the skill is under cool down then we cant cast it again
				if(skill1CoolDown==true)
				{
					return;
				}
				//print("show area");
				//if we havent creat the castarea then spawn it and goto casting state
				if(affectArea == null)
				{
					affectArea =  Instantiate(affectAreaPrefab[type],affectAreaPosition,Quaternion.identity ) as GameObject;
					isCasting = true;
				}
			}
			else if(type == 1)
			{
				if(skill2CoolDown==true)
				{
					return;
				}
				CastSkill(sName,type);
			}
			else if(type == 2)
			{
				if(skill3CoolDown==true)
				{
					return;
				}
				CastSkill(sName,type);
			}
		}	
	}
	
	//spawn the skill object
	public void CastSkill(string sName,int type)
	{
		Vector3 modifyY = new Vector3(0,1,0); 
		//if the affectarea exits then spawn the skill object there
		//otherwise spawn it with player
		if(affectArea!=null)
		{
			castPosition = affectArea.transform.position;
			GameObject skillObject = Instantiate(skillPrefab[type],castPosition+modifyY,Quaternion.identity)as GameObject;
			skillObject.transform.LookAt(transform.position);
			//print (skillObject+"  "+castPosition);
			
		}
		else
		{
			if(type != 0)
			{
				Instantiate(skillPrefab[type],transform.position,Quaternion.identity);
			}
		}
	}
	
	/*
	private float GetSkillRotation()
	{
		
		//Vector2 castPos = new Vector2(castPosition.x,castPosition.z);
		//Vector2 startPos = new Vector2(transform.position.x,transform.position.z);
		
		float dirX = Vector3.Angle(castPosition.forward,transform.forward);
		print ("and"+ "   result+"+dirX);
		
		return dirX;
	}
	*/
	
	//update the position of affectarea with raycasting
	private void UpdateAreaPosition()
	{
		myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if(Physics.Raycast(myRay,out myHit,1000))
		{
			//if ray hit ground 
			if(myHit.transform.tag == "ground")
			{
				//pass the hit position to the affectarea position
				affectAreaPosition = myHit.point;
				//if affectarea isnt null then update it's position
				if(affectArea!=null)
				{
					affectArea.transform.position = affectAreaPosition;
				}
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
			skillTimer[type] = 0;
			if(type == 0)
			{
				skill1CoolDown = false;
				ResetTimer(type);
			}
			if(type == 1)
			{
				skill2CoolDown = false;
				ResetTimer(type);
			}
			if(type == 2)
			{
				skill3CoolDown = false;
				ResetTimer(type);
			}
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
		GUI.BeginGroup(new Rect(Screen.width/2-100,Screen.height -150,300,80));
		//if skills are enabled then show the buttons
		if(enableSkill1 == true)
		{
			skillButtons[0] = GUI.Button(new Rect(ButtonPosX,ButtonPosY,ButtonWidth,ButtonHeight),skillIcon[0]);
		}
		if(enableSkill2 == true)
		{
			skillButtons[1] = GUI.Button(new Rect(ButtonPosX+xModifier,ButtonPosY,ButtonWidth,ButtonHeight),skillIcon[1]);
		}
		if(enableSkill3 == true)
		{
			skillButtons[2] = GUI.Button(new Rect(ButtonPosX+xModifier*2,ButtonPosY,ButtonWidth,ButtonHeight),skillIcon[2]);
		}
		//set the cooldown timer label font color to red
		GUI.contentColor = Color.red;
		//if the skills are in cool down then show the cooldown icon and label 
		if(skill1CoolDown == true)
		{
			GUI.DrawTexture(new Rect(ButtonPosX,ButtonPosY,ButtonWidth,ButtonHeight),skillCoolDownIcon[0]);
			GUI.Label(new Rect(ButtonPosX+labelXModifier,ButtonPosY+yModifier,ButtonWidth,ButtonHeight),""+Mathf.FloorToInt( skillTimer[0]));
		}
		
		if(skill2CoolDown == true)
		{
			GUI.DrawTexture(new Rect(ButtonPosX+xModifier,ButtonPosY,ButtonWidth,ButtonHeight),skillCoolDownIcon[1]);
			GUI.Label(new Rect(ButtonPosX+xModifier+labelXModifier,ButtonPosY+yModifier,ButtonWidth,ButtonHeight),""+Mathf.FloorToInt( skillTimer[1]));
		}
		
		if(skill3CoolDown == true)
		{
			GUI.DrawTexture(new Rect(ButtonPosX+xModifier*2,ButtonPosY,ButtonWidth,ButtonHeight),skillCoolDownIcon[2]);
			GUI.Label(new Rect(ButtonPosX+xModifier*2+labelXModifier,ButtonPosY+yModifier,ButtonWidth,ButtonHeight),""+Mathf.FloorToInt( skillTimer[2]));
		}
		GUI.EndGroup();
	}
}
