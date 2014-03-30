using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour 
{
	public bool isClicked;//check it the item is clicked
	// Use this for initialization
	void Start () 
	{
		isClicked = false;
	}
	
	//if the item is clicked turn off the render of it
	void OnMouseDown()
	{
		isClicked = true;
		renderer.enabled = false;
	}
}
