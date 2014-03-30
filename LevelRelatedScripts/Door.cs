using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
	private LevelManager levelManager;//varible to access the level manager
	private string doorText = "You need to find a KEY(purple sphere) to open the door!";//the text when we collide with the door
	
	void Start()
	{
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();//access the level manager
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player"||other.tag == "Minion")
		{
			StartCoroutine(levelManager.ShowGameStatsText(doorText));//change the text to what we want
		}
	}
}
