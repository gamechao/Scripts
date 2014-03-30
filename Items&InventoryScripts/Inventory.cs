using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
	public List<Transform> Contents;//contents in inventory
	
	public void AddItem(Transform Item)
	{
		Contents.Add(Item);
		//print (Item.name + "has been added to inventory");
		//DebugInfo();
	}

	public void RemoveItem(Transform Item)//function to remove the item from inventory
	{
		for(int i= 0;i <Contents.Count;i++)
		{
			if(Contents[i] == Item)
			{
				Contents.RemoveAt(i);
			}
		}
	}
	
	public void DebugInfo()//for debug use
	{
		print ("this is for debugging ");
		for(int i=0;i<Contents.Count;i++)
		{
			print (Contents[i].name);
		}
		print("Inventory contains all these items");
	}
	
}
