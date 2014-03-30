using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
	public float CamSpeed = 0.8f;
	public float ScrollSpeed = 0.1f;
	public int GUIsize = 25;
	public float yFixer = 25.0f;
	public float tolerance = 2.0f;
	private GameObject curPlayer;

	void Start()
	{
		curPlayer = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () 
	{
		UpdateCamera();
		if(Input.GetKey("space"))
		{
			CenterCameraToPlayer();
		}
	}
	

 
	void UpdateCamera () 
	{
		//make detecting rect
		Rect recdown = new Rect (0, 0, Screen.width, GUIsize);
		Rect recup = new Rect (0, Screen.height-GUIsize, Screen.width, GUIsize);
		Rect recleft = new Rect (0, 0, GUIsize, Screen.height);
		Rect recright = new Rect (Screen.width-GUIsize, 0, GUIsize, Screen.height);
 		//move camera
    	if (recdown.Contains(Input.mousePosition))
		{
        	transform.Translate(0, 0, -CamSpeed, Space.World);
		}
 
    	if (recup.Contains(Input.mousePosition))
		{
        	transform.Translate(0, 0, CamSpeed, Space.World);
		}
 
    	if (recleft.Contains(Input.mousePosition))
		{
        	transform.Translate(-CamSpeed, 0, 0, Space.World);
		}
 
    	if (recright.Contains(Input.mousePosition))
		{
        	transform.Translate(CamSpeed, 0, 0, Space.World);
		}

		if (transform.position.y > curPlayer.transform.position.y + yFixer + tolerance)
		{
			transform.Translate(0, -ScrollSpeed , 0, Space.World) ;
		}
		else if(transform.position.y < curPlayer.transform.position.y + yFixer - tolerance)
		{
			transform.Translate(0, ScrollSpeed , 0, Space.World) ;
		}

	}

	void CenterCameraToPlayer()
	{
		Vector3 tempPos;
		tempPos.x = curPlayer.transform.position.x;
		tempPos.y = this.transform.position.y;
		tempPos.z = curPlayer.transform.position.z;
		transform.position = tempPos;
	}
	
}
