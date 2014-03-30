using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
	//player
	public GameObject player;
	
	//get the stats
	public EnemyStatsBasic enemyStats;
	
	//projectile
	public GameObject projectile;//projectile prefab
	protected GameObject curProjectile;//track current projectile
	
	//state machine
	public StateMachine stateMachine = null;
	
	//tuning
	public float speed = 0.5f;
	public float idleDistance = 10f;
	public float attackDistance = 4f;
	
	//scirpt use varibles
	public bool canAttack = false;
	public bool isExploder =false;
	public float interval;
	public float distance;
	private Vector3 startPos;
	
	//fight related varibles
	public int damage;
	public float damageReduce;
	public float dodge;
	
	//gui
	public Texture2D hpBarTexture;
	private float adjustment = 2.3f;
	private float hpBarLength;
	private float hpBarWidth = 5f;
	private Vector3 worldPosition;
	private Vector3 screenPosition;
	public Camera myCamera;
	private int hpBarLeft = 70;
	private int  barTop = 1;
	
	//public Vector3 posModifier;
	// Use this for initialization
	protected virtual void Start () 
	{
		myCamera = GameObject.Find("Camera").GetComponent<Camera>();
		//find the player
		player = GameObject.FindGameObjectWithTag("Player");
		enemyStats = new EnemyStatsBasic();
		startPos = this.transform.position;
		//print (enemyStats.enemyHealth);
		//if is exploder set varible to true
		if(this.tag == "Exploder")
		{
			isExploder = true;
		}
		//find the state machine and set the init state
		stateMachine = GetComponent<StateMachine>();
		stateMachine.SetState(new BasicIdleState(this));
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		//update distance
		GetDistance(player);
		//update states
		SetToAppropriateState();
		//update health
		CalculateHp();
		//destroy while hp <0
		Die();

	}
	
	//function to toggle the states
	private void SetToAppropriateState()
	{
		GotoMoveState();
		GotoIdleState();
	}
	
	//change to move state
	public void GotoMoveState()
	{
		//if player is out of attack range but not far enough to reach idle distance
		if(distance>attackDistance&&distance<idleDistance)
		{
			//if cant attack
			if(canAttack == false)
			{
				//print ("move");
				//update the state to move
				stateMachine.SetState(new BasicMoveState(this));
			}
		}
	}
	
	//change to attack state
	public void GotoAttackState()
	{
		//print (attackDistance);	
		stateMachine.SetState(new BasicAttackState(this));
	}
	
	//change to idle state
	public void GotoIdleState()
	{
		//if player in idle distance
		if(distance>idleDistance)
		{
			//print ("idle");
			stateMachine.SetState(new BasicIdleState(this));
		}
	}
	

	
	//function to move enemy
	public virtual void Move()
	{
		//print (distance);
		Vector3 CurrentPos = transform.position;
		if(distance<idleDistance)
		{
			Vector3 moveStep = player.transform.position - transform.position;//get direction
			moveStep.Normalize();
			
			transform.LookAt(player.transform.position);//rotate enemy
			transform.position = CurrentPos + (moveStep * speed * Time.deltaTime);//move
			//GetDistance(player);
		}
		else
		{
			Vector3 moveStep = startPos - transform.position;//get direction
			moveStep.Normalize();
			
			transform.LookAt(startPos);//rotate enemy
			transform.position = CurrentPos + (moveStep * speed * Time.deltaTime);//move
		}
	}
	
	public virtual void LookAtPlayer()
	{
		transform.LookAt(player.transform.position);
	}
	
	//get distance	
	public void GetDistance(GameObject curplayer)//function to get distance between player and targetpoint
	{
		distance = Vector3.Distance(transform.position,curplayer.transform.position);
	}
	
	//spawn projectile while attack
	public virtual IEnumerator Attack()
	{				
		curProjectile = Instantiate(projectile,this.transform.position,this.transform.rotation) as GameObject;
		curProjectile.GetComponent<BasicProjectile>().SetAmount(damage);
		yield return new WaitForSeconds(interval);
		
		//print ("fire");
	}
	
	//destroy enemy while hp is less than 0
	public virtual void Die()
	{
		if(enemyStats.enemyHealth <= 0)
		{
			Destroy(gameObject);
		}
	}
	
	//check projectile if exits
	public bool TrackProjectile()
	{
		if(curProjectile)
		{
			canAttack = false;
			return true;
		}
		else
		{
			canAttack =true;
			return false;
		}
	}
	
	//do nothing while idle
	public virtual void Idle()
	{
		Move();
		//for future use  cause enemy shouldn't stay still while idle
	}
	
	//modify health bar length
	private void CalculateHp()
	{
		float hpPercent;
		hpPercent = enemyStats.enemyHealth/enemyStats.enemyMaxHealth;
		hpBarLength = hpPercent*50;
	}
	
	//show health bar
	void OnGUI()
	{
		worldPosition = new Vector3(transform.position.x, transform.position.y + adjustment,transform.position.z);
		screenPosition = myCamera.WorldToScreenPoint(worldPosition);
		 GUI.DrawTexture(new Rect(screenPosition.x - hpBarLeft / 2,Screen.height - screenPosition.y - barTop,hpBarLength, hpBarWidth), hpBarTexture);
	}
}

