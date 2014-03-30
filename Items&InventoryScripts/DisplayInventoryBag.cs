using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplayInventoryBag : MonoBehaviour 
{
	public Item itemBeingDragged;//the item being dragged
	Vector2 draggedItemPosition;//the item position on screen while dragging
	Vector2 draggedItemSize;//item icon while dragging
	
	
	public PlayerScript player;//varible for getting player

	private Vector2 windowSize = new Vector2(200,200);//the window size
	private Vector2 itemIconSize = new Vector2(32.0f,32.0f);//item icon
	public List<Transform> UpdateList;//list for update the inventory
	public Inventory associatedInventory;//access the inventory
	public bool displayInventory =false;//show inventory or not
	public GUISkin invSkin;//gui skin
	public Rect windowRect = new Rect(200,200,108,130);//the inventory window
	public Texture2D bagIcon;//the inventory icon
	public int offsetX = 6;//button X offset
	public int offsetY = 6;//button Y offset
		
	// Use this for initialization
	void Start () 
	{
		windowRect=new Rect(Screen.width-windowSize.x-80,Screen.height-170-windowSize.y,windowSize.x,windowSize.y);//create the window
		associatedInventory=GetComponent<Inventory>();//get access to our inventory
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Mouse1))//if right click 
		{
			ClearDraggedItem();//get rid of the dragged item.
		}
		//print (Time.time);
		
		if(itemBeingDragged!=null)//if we have a item being dragged make it follow mouse
		{
			draggedItemPosition.y = Screen.height-Input.mousePosition.y+15;
			draggedItemPosition.x = Input.mousePosition.x + 15;
		}
		
		UpdateInventoryList();
		
	}
	
	public void UpdateInventoryList()//updating the inventory
	{
		//print("assnum"+associatedInventory.Contents.Count);
		UpdateList = associatedInventory.Contents;
		//print("update"+UpdateList.Count);
	}
	
	public void OnGUI()
	{
		GUI.skin = invSkin;
		if(itemBeingDragged != null)
		{
			GUI.Button(new Rect(draggedItemPosition.x,draggedItemPosition.y,draggedItemSize.x,draggedItemSize.y),itemBeingDragged.InvIcon);
		}
		
		if(bagIcon != null)
		{
			bool bagButton = GUI.Button(new Rect(Screen.width-110,Screen.height -150,40,40),new GUIContent(bagIcon,"Inventory"));
			GUI.Label(new Rect(Screen.width-110,Screen.height-170,100,20), GUI.tooltip);
			if(bagButton)
			{	
				if(displayInventory)
				{
					displayInventory = false;
					ClearDraggedItem();
					//we dont want player to move while inventory on cause we need right click to use items in inventory
					if(player.curPlayer!=null)
					{
						player.isSelected = true;
					}
				}
				else
				{
					displayInventory = true;
					if(player.curPlayer!=null)
					{
						//print ("player not null");
						player.isSelected = false;
					}
				}
			}
		}
		
		if(displayInventory)
		{
			windowRect = GUI.Window(0,windowRect,DisplayInventoryWindow,"Inventory");
			windowSize =new Vector2(windowRect.width,windowRect.height);
		}
	}
	
	/*void FixedUpate()
	{
		if(itemBeingDragged!=null)
		{
			draggedItemPosition.y = Screen.height-Input.mousePosition.y+15;
			draggedItemPosition.x = Input.mousePosition.x + 15;
		}
		if(Time.time>lastUpdate)
		{
			lastUpdate = Time.time + updateListDelay;
			UpdateInventoryList();
		}
	}*/
	
	public void DisplayInventoryWindow(int windowID)
	{
		float currentX = 0 + offsetX;
		float currentY = 0 + offsetY;
		//print (currentX);
		//print("Update"+UpdateList.Count);
		
		
		for(int i = 0; i< UpdateList.Count;i++)
		{
			Item item = UpdateList[i].GetComponent<Item>();
			//print(t.name);
			if(item.stack <= 0)
			{
				UpdateList.Remove(UpdateList[i]);
			}
			GUI.skin = invSkin;
			bool itemButton = GUI.Button(new Rect(currentX,currentY,itemIconSize.x,itemIconSize.y),new GUIContent(item.InvIcon,item.itemDescription));
			GUI.Label(new Rect(10,20,300,300),GUI.tooltip);
			GUI.depth = 0;
			GUI.tooltip = "";
			if(itemButton)
			{
				//print("have item");
				if(Event.current.button == 0)
				{
					bool dragitem = true;
					
					if(itemBeingDragged == item)//if the item is the dragged one put it back to the slot
					{
						ClearDraggedItem();
						dragitem = false;
					}
					if(dragitem)
					{
						itemBeingDragged = item;
						draggedItemSize = itemIconSize;
						draggedItemPosition.y = Screen.height-Input.mousePosition.y-15;
						draggedItemPosition.x = Input.mousePosition.x+15;
					}
				}
				else
				{
					//print ("right click");
					if(item.stack>0)
					{
						item.UseItem();
					}
				}
			}
			if(item.stackable)//if stackable then update the stack
			{
				GUI.Label(new Rect(currentX,currentY,itemIconSize.x,itemIconSize.y),""+item.stack,"Stacks");
				
			}
			currentX += itemIconSize.x;
			if(currentX+itemIconSize.x+offsetX>windowSize.x)
			{
				currentX = offsetX;
				currentY += itemIconSize.y;
				if(currentY+itemIconSize.y+offsetY>windowSize.y)//check if inventory is full
				{
					return;
				}
			}
		}
		
	}
	/*
	private void UseInventoryItem(Item item)
	{
		if(item.isEquipment == false)
		{
			if(item.stackable == true)
			{
				if(item.stack > 1)
				{
					item.subtractStack();
				}
			}
		}
	}*/
	
	public void ClearDraggedItem()
	{
		itemBeingDragged = null;
	}
}
