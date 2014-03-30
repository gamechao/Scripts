using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour 
{
	//player information
	public PlayerReplicationInfo curPlayerStats;
	public PlayerScript player;
	
	//tuning valve in editor
	public Texture2D InvIcon;  //inventory button icon
	public bool canGet = true; //if the item can be get
	public string itemType;    //item type
	public bool stackable = false;//if the item is stackable
	public int maxStack = 99; //the max stack
	public int stack = 1;     //the stack of item
	public bool isEquipment =  true; //if the item is a equipment
	public string itemDescription;//the item description
	
	protected virtual void Start () 
	{
		//access player information
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
		curPlayerStats = player.playerStats;
		//print (curPlayerStats);
	}
	
	
	//when player click on item
	protected virtual void OnMouseDown()
	{
		bool getit = true;
		Inventory playersinv =null ;
		playersinv = FindObjectOfType(typeof(Inventory)) as Inventory;
		
		if(canGet)//check if the item can be get
		{
			if(stackable)//check if the item is stackable
			{
				Item locatedit = null;//item to be located
				foreach (Transform t in playersinv.Contents)//check if the item is in inventory
				{
					if(t.name == this.transform.name)
					{
						Item i = t.GetComponent<Item>();
						if(i.stack < i.maxStack)
						{
							locatedit = i;
						}
					}
				}
				if(locatedit != null)
				{
					getit = false;
					locatedit.stack += 1;
					Destroy(this.gameObject);
				}
				else
				{
					getit = true;
				}
			}
			if(getit)
			{
				playersinv.AddItem(this.transform);
				MoveToPlayer(playersinv.transform);
				//print(playersinv.Contents.Count);
			}	
		}	
	}
	//gonna be a function which bring item to player in vision
	void MoveToPlayer(Transform thePlayer)
	{
		canGet = false;
		transform.collider.isTrigger = true;
		transform.parent = thePlayer;
		transform.localPosition = Vector3.zero;	
	}
	//the function to use item
	public virtual void UseItem()
	{
		
	}
	//the function to update the stack
	public void subtractStack()
	{
		stack -= 1;
	}
	
}
