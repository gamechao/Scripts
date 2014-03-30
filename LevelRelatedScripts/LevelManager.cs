using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	//in level objects
	public GameObject doorOne;
	public GameObject doorTwo;
	public GameObject keyOne;
	public GameObject keyTwo;
	public GameObject goal;
	
	//camera
	public Camera gameCamera;
	
	//game infomation text
	public GUIText gameStatsText;
	
	//help button
	private bool helpButton;
	private bool displayHelp;
	
	//player information
	private GameObject playerInLevelManager;
	private PlayerReplicationInfo playerInfoInLevelManager;
	private string gameStartInfo = "Use Mouse Left Click to Gather SummonResource And Potion." +"\n"+
		"Then Click Minion Button to Summon Minions to help you fight." +"\n"+
		"You can also use Formation Button to change the position of your minions at any time.";
	
	// Use this for initialization
	void Start () 
	{
		//access the player
		playerInLevelManager = GameObject.Find("Player");
		playerInfoInLevelManager = playerInLevelManager.GetComponent<PlayerScript>().playerStats;
		//show game start text
		StartCoroutine(ShowGameStatsText(gameStartInfo));
	}
	
	// Update is called once per frame
	void Update () 
	{
		//check if we get the key
		CheckDoors();
		//check if the player is alive
		CheckPlayerStats();
		//toggle help information
		if(helpButton)
		{
			
			//print (GUI.tooltip);
			//print ("make button");
			if(displayHelp)
			{
				displayHelp = false;
			}
			else
			{
				displayHelp = true;
			}
		}
	}
	
	//tracing the keys and manage with our doors
	private void CheckDoors()
	{
		if(keyOne.GetComponent<Key>().isClicked ==true)
		{
			Destroy(doorOne);
		}
		if(keyTwo.GetComponent<Key>().isClicked ==true)
		{
			Destroy(doorTwo);
		}
		if(goal.GetComponent<Key>().isClicked ==true)
		{
			gameCamera.enabled = false;
			gameStatsText.text = "You Win!!!!!";
		}
	}

	//if the player died we lose
	private void CheckPlayerStats()
	{
		if(playerInfoInLevelManager.playerHealth<0)
		{
			gameStatsText.text = "You Lose!!!!!!";
		}
	}
	
	//function for changing the game information text
	public IEnumerator ShowGameStatsText(string content)
	{
		gameStatsText.text = content;
		yield return new WaitForSeconds(10.0f);
		gameStatsText.text = "";
	}
	
	void OnGUI()
	{
		helpButton = GUI.Button(new Rect(30,110,40,40),"Help");
		if(displayHelp)
		{
			GUI.color = Color.red;
			GUI.Box(new Rect(30,150,500,180),"Game Control:\n " +
				"YOU CAN NOT MOVE WITH INVENTORY OPENED!!!!!\n" + 
				"Left Click to Select Player and get items\n" +
				"Right Click to move and USE items in inventory\n" + 
				"Minions:\n" + 
				"Melee:High Constitution, Low damage, 10 Resource summon cost\n" + 
				"Range:Medium Constitution, Medium damage, 15 Resource summon cost\n"+
				"Magic:Low Constitution, High damage, 20 Resource summon cost\n"+
				"Heal:Medium Constitution, No damage(has heal skill), 30 Resource summon cost\n");
		}
	}
}
