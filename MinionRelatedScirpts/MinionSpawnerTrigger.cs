using UnityEngine;
using System.Collections;

public class MinionSpawnerTrigger : MonoBehaviour 
{
	GameObject gameMaster;
	// Use this for initialization
	void Start () 
	{
		gameMaster = GameObject.Find("GameMaster");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			gameMaster.GetComponent<DisplayMinion>().isEnabled = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			gameMaster.GetComponent<DisplayMinion>().isEnabled = false;
		}
	}
}
