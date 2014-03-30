using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour 
{
	public Ray myRay;
	public RaycastHit[] myHit;
	public bool isSelected = false;//check if player is selected
	public GameObject curPlayer;//current player
	public int speed ;//move speed
	public Color colorWhileSelected;
	
	private Vector3 movePoint = new Vector3(0,1f,0);//targetpoint
	private float distance = 0;//distance between target move point and player
	
	public PlayerReplicationInfo playerStats;
	
	//fight related varibles
	public int damage;
	public float damageReduce;
	public float dodge;
	private StatsTransfer playerStatsTransfer = new StatsTransfer();
	
	// Use this for initialization
	void Awake () 
	{
		//access  player stats
		playerStats = new PlayerReplicationInfo();
		//print (playerStats);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//update current health so it wont go beyond max health 
		playerStats.FixHealth();
		//update the selection
		makeSelection();
		//moving
		if(isSelected == true)
		{
			//if player is selected we can move
			if(Input.GetKeyDown (KeyCode.Mouse1))
			{
				//get move point from raycasting
				movePoint = RayCastLineTest(curPlayer.transform.position);
				//get distance
				GetDistance(curPlayer);
			}
			if(movePoint!=curPlayer.transform.position)
			{
				//if player havent reach target move point then keep on moving
				move();
			}
		}
		//update fight stats
		damage = playerStatsTransfer.CalculateDamage(playerStats.playerStrength,playerStats.level);
		damageReduce = playerStatsTransfer.CalculateDamageReducePercentage(playerStats.playerConstitution);
		dodge = playerStatsTransfer.CalculateDodge(playerStats.playerAgility);
		//print (damage+"   "+damageReduce+"   "+dodge);
	}
	
	//ray tracing to check if we left click on player
	private void makeSelection()
	{
		//selection
		if(Input.GetKeyDown (KeyCode.Mouse0))
		{
			myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			myHit=Physics.RaycastAll(myRay,  1000);
			int i =0;
			while(i<myHit.Length)
			{
				RaycastHit hit = myHit[i];
				if(hit.transform.tag=="Player")
				{
					//check if current player is null or not
					if(curPlayer == null)
					{
						//assign the player we clicked to current player
						curPlayer = hit.transform.gameObject;
						if(curPlayer!=null)
						{
							//if player if selected change the color
							curPlayer.renderer.material.color= colorWhileSelected;
							movePoint = curPlayer.transform.position;
						}
						isSelected = true;
						//print("selected"); 
					}
				}
				i++;
			}
				////////for future use
				/*
				else
				{
					if(curPlayer!=null)
					{
						curPlayer.renderer.material.color= new Color(1,1,1);
					}
					curPlayer = null;
					isSelected = false;					
				}*/
		}
	}
	
	
	
	/////////////////////////////////move function with time  no more use//////////////////////////////////////////////////////
	/*
	private void MoveTo(GameObject obj, Vector3 newPos)
	{
		
	
	}
	
	private void move()
	{
		myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(myRay, out myHit, 1000))
		{
			//print(myHit.transform.tag);
			if(myHit.transform.tag == "ground")
			{
			
				Vector3 movePoint = myHit.point;
				Quaternion moveRotation = Quaternion.LookRotation(movePoint - curPlayer.transform.position);
				movePoint.y += 1; // hack to shift origin
				curPlayer.transform.rotation = moveRotation;
				MoveTo(curPlayer, movePoint);
			}
		}
 		
	}*/
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void move()//function to move player
	{
		Vector3 playerCurrentPos = curPlayer.transform.position;
		if(distance > 1f)
		{
			
			Vector3 moveStep = movePoint - curPlayer.transform.position;//get direction
			moveStep.Normalize();//get distance
			
			//smooth rotation
			float degsPerSec = 3.0f;//rotation step
			Quaternion limitedRotation = new Quaternion(0f,0f,0f,0f);//varible to limit the rotation
			Quaternion rotation = Quaternion.LookRotation(movePoint-curPlayer.transform.position);
			rotation.eulerAngles = new Vector3(0,rotation.eulerAngles.y,0);
			limitedRotation = Quaternion.RotateTowards(curPlayer.transform.rotation, rotation, degsPerSec);
			curPlayer.transform.rotation = limitedRotation;
			
			//curPlayer.transform.LookAt(movePoint);//rotate player
			curPlayer.transform.position = playerCurrentPos + (moveStep * speed * Time.deltaTime);//move
			GetDistance(curPlayer);
		}
		
	}
	
	//function to get distance between player and targetpoint
	private void GetDistance(GameObject player)
	{
		distance = Vector3.Distance(player.transform.position,movePoint);
	}
	
	//function to get the targetpoint
//	public Vector3 RayCastLineTest(Vector3 playerLoc)
//    {
//		//cast a new ray for movement
//        RaycastHit[] moveRayhit ;
//        Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition)  );
//        moveRayhit = Physics.RaycastAll(ray, 60);
//		
//		
//		for(int i = 0;i < moveRayhit.Length;i++)
//		{
//			RaycastHit hit = moveRayhit[i];
//	        Debug.DrawLine(Camera.main.transform.position, hit.point);
//			bool moveFlag = false;
//			//print (hit.transform.tag);
//
//			for(int l=0;l<i;l++)
//			{
//				if(moveRayhit[moveRayhit.Length-1].transform.tag == "ground")
//				{
//					moveFlag = true;
//				}
//			}
//			print (i+" + "+moveRayhit[i].collider.tag);
//			//print (curPlayer.transform.position);
//
//			if(moveFlag == true)
//			{
//				playerLoc = new Vector3(hit.point.x,hit.point.y + 1.0f,hit.point.z);
//			}
//			else
//			{
//				playerLoc = curPlayer.transform.position;
//			}
//		}
//		return playerLoc;
//    }
	public Vector3 RayCastLineTest(Vector3 playerLoc)
	{
		//cast a new ray for movement
		RaycastHit hit ;
		Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition)  );
		if(Physics.Raycast(ray, out hit,60))
		{

			if(hit.transform.tag == "ground")
			{
				playerLoc = new Vector3(hit.point.x,hit.point.y + 1.0f,hit.point.z);
			}

			else
			{
				playerLoc = curPlayer.transform.position;
			}
		}
		return playerLoc;
	}
}
