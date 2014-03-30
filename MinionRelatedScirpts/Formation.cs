using UnityEngine;
using System.Collections;

public class Formation : MonoBehaviour 
{
	public GameObject[] minions = new GameObject[10];//array of spawner
	public int formType = 0;//the type of formation
	public GameObject spawnerPref;//the spawner prefab
	private GameObject player;//the player
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");//get the player
		InitFormation();//initiate the formation
		ChangeFormation(formType);//set the formation to default
		HideFormation();
	}
	
	void Update()//for test the function
	{
		if(Input.GetKey("s"))
		{
			ShowFormation();
		}
		if(Input.GetKey("d"))
		{
			HideFormation();
		}
	}
	

	public void InitFormation()
	{
		for(int i = 0; i<minions.Length;i++)//assign prefab to our array and parent them
		{
			minions[i]=
			Instantiate(spawnerPref,transform.position,Quaternion.identity)as GameObject;
			minions[i].transform.parent = player.transform;
		}
	}
	
	public void ShowFormation()
	{
		for(int i = 0; i<minions.Length; i++)
		{
			minions[i].renderer.enabled = true;
		}
	}
	
	public void HideFormation()
	{
		for(int i = 0; i<minions.Length; i++)
		{
			minions[i].renderer.enabled = false;
		}
	}
	
	public void ChangeFormation(int type)//function to change formation
	{
		if(type == 0)
		{
			int index = 0;
			float multi = 1f;//position multiplier
			int colPos = 0;//position in current row
			for(int row = 0; row< 4 ; row++)
			{
				if(row%2==1)
				{
					colPos = -row;
					for(int col = 0; col < row+1;col++)
					{					
						minions[index].transform.localPosition = new Vector3(colPos*multi,0,(row+1)*multi);
						index++;
						colPos += 2;			
					}
				}
				else
				{
					colPos = -row;
					for(int col = 0; col < row+1;col++)
					{		
						minions[index].transform.localPosition = new Vector3(colPos*multi,0,(row+1)*multi);
						index++;
						colPos += 2;
					}
				}
			}
		}
		//the second formation
		if(type == 1)
		{
			int index = 0;
			float multi = 1f;
			int colPos = 0;
			for(int row =0; row<3;row++)
			{
				if(row%2 == 0)
				{
					colPos = -3;
					for(int col = 0; col<4;col++)
					{
						minions[index].transform.localPosition = new Vector3(colPos*multi,0,(row-1)*multi);
						index++;
						colPos += 2;
					}
				}
				else
				{
					colPos = -3;
					for(int col = 0; col<2;col++)
					{
						minions[index].transform.localPosition = new Vector3(colPos*multi,0,(row-1)*multi);
						index++;
						colPos += 6;
					}
				}
			}
		}
		//the third formation
		if(type == 2)
		{
			int index = 0;
			float multi = 1f;
			int colPos = 0;
			for(int row =0; row<3;row++)
			{
				if(row == 0)
				{
					colPos = -3;
					for(int col = 0; col<4;col++)
					{
						minions[index].transform.localPosition = new Vector3(colPos*multi,0,(row-1)*multi);
						index++;
						colPos += 2;
					}
				}
				else if(row == 2)
				{
					colPos = -5;
					for(int col = 0; col<6;col++)
					{
						minions[index].transform.localPosition = new Vector3(colPos*multi,0,(row-1)*multi);
						index++;
						colPos += 2;
					}
				}
			}
		}
	}
}
