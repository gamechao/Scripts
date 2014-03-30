using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInv : MonoBehaviour 
{
	public List<Item> ArmorSlots;//list of equipment
	public List<string> ArmorSlotsName;//list of the names of slots
	private bool csheet = false;//show character window or not 
	public List<Rect> positionsForCS;//buttons in character window
	public Texture2D cbutton;//button of character window
	
	//Window property
	private int WindowPosX = 100;
	private int WindowPosY = 100;
	private int WindowWidth = 200;
	private int WindowHeight = 300;
	private Rect windowRect;
	
	//Button property
	public int ButtonPosX = 0;
	public int ButtonPosY = 40;
	public int ButtonWidth = 40;
	public int ButtonHeight = 40;
	
	//player skill
	private PlayerSkill curPlayerSkill;
	private PlayerReplicationInfo curPlayerStats;
	
	void Start () 
	{
		curPlayerSkill = GetComponent<PlayerSkill>();
		curPlayerStats = GetComponent<PlayerScript>().playerStats;
		SetArmorSlotsName();//initiate the name of slots
		windowRect = new Rect(WindowPosX,WindowPosY,WindowWidth,WindowHeight);//character window
	}
	
	//funtion to set the names of the slots
	public void  SetArmorSlotsName()
	{
		ArmorSlotsName.Add("Head");
		ArmorSlotsName.Add("Body");
		ArmorSlotsName.Add("Foot");
		ArmorSlotsName.Add("LH");
		ArmorSlotsName.Add("RH");
	}
	
	//check if slot is uesed 
	public bool CheckSlot(int tocheck)
	{
		bool toreturn = false;
		if(ArmorSlots[tocheck]!=null)
		{
			toreturn = true;
		}
		return toreturn;
	}
	
	//function to bring equipment to slot(for other script use)
	/*
	public void UseItem(Item i,int slot)
	{
		if(i.isEquipment)//check if the item is an equipment
		{
			if(i.itemType == ArmorSlotsName[slot])//check if the slot name matches item type
			{
				EquipItem(i,slot);
				EnableSkill(i);
				print ("item eqiup+"+i.name);
			}	
		}
	}*/
	
	//function to equip item
	public void EquipItem(Item i,int slot)
	{
		if(i.itemType == ArmorSlotsName[slot])
		{
			if(CheckSlot(slot))
			{
				UnequipItem(ArmorSlots[slot],slot);
				ArmorSlots[slot] = null;
			}
			ArmorSlots[slot] = i;
			EnableSkill(i);
			EnhanceStats(i);
			i.transform.position = transform.position;
			i.transform.rotation = transform.rotation;
		}
	}
	
	//function to unequip
	public void UnequipItem(Item i,int slot)
	{
		//print (i);
		i.enabled = false;
		ArmorSlots[slot] = null;
	}
	
	private void OnGUI()
	{
		if(cbutton!=null)//if button icon exists
		{
			bool characterButton = GUI.Button(new Rect(ButtonPosX,Screen.height-ButtonPosY,ButtonWidth,ButtonHeight),new GUIContent(cbutton,"Character Window"));
			GUI.Label(new Rect (ButtonPosX,Screen.height-ButtonPosY-20,120,20),GUI.tooltip);
			if(characterButton)
			{
				if(csheet)
				{
					csheet = false;
				}
				else
				{
					csheet = true;
				}
			}
		}
		if(csheet)
		{
			windowRect = GUI.Window(1,windowRect,DisplayCSheetWindow,"Character Sheet");
		}
	}
	
	private void EnableSkill(Item item)
	{
		if(item.name == "Stick")
		{
			curPlayerSkill.enableSkill1 = true;
		}
		if(item.name == "Book")
		{
			curPlayerSkill.enableSkill2 = true;
		}
		if(item.name == "Boots")
		{
			curPlayerSkill.enableSkill3 = true;
		}
	}
	
	private void EnhanceStats(Item item)
	{
		if(item.name == "hat")
		{
			curPlayerStats.playerConstitution += 5;
		}
		if(item.name == "armor")
		{
			curPlayerStats.playerConstitution += 10;
		}
	}
	
	private void DisplayCSheetWindow(int windowID)
	{
		GUI.DragWindow(new Rect(0,0,10000,20));//make character window can drag
		for(int i=0;i<ArmorSlots.Count;i++)//loop to check the states of each slots
		{
			if(ArmorSlots[i] == null)
			{
				//GUI.color = Color.white;
				if(GUI.Button(positionsForCS[i],ArmorSlotsName[i]))
				{
					DisplayInventoryBag id = GetComponent<DisplayInventoryBag>();
					if(id.itemBeingDragged != null)
					{
						EquipItem(id.itemBeingDragged,i);
						id.ClearDraggedItem();
					}
				}
			}
			else
			{
				bool eqiupmentButton = GUI.Button(positionsForCS[i],new GUIContent(ArmorSlots[i].InvIcon,ArmorSlots[i].itemDescription));
				//GUI.color = Color.red;
				GUI.Label(new Rect(5,20,300,300),GUI.tooltip);
				GUI.tooltip = "";
				if(eqiupmentButton)
				{
					DisplayInventoryBag id2 = GetComponent<DisplayInventoryBag>();
					if(id2.itemBeingDragged != null)
					{
						EquipItem(id2.itemBeingDragged,i);
						id2.ClearDraggedItem();
					}
					else
					{
						UnequipItem(ArmorSlots[i],i);
						id2.ClearDraggedItem();
					}
				}
			}
		}
	}
}
