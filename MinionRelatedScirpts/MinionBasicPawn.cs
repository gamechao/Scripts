using UnityEngine;
using System.Collections;

public abstract class MinionBasicPawn : MonoBehaviour 
{
	//player
	public GameObject player;
	
	//enemy
	public GameObject currentEnemy;
	
	//get the stats
	public MinionBasicStats minionStats;
	
	//get the summoned minions
	public DisplayMinion minionsSummoned;
	
	//projectile
	public GameObject projectile;//projectile prefab
	public GameObject curProjectile;//track current projectile
	
	//fight related varibles
	public int damage;
	public float damageReduce;
	public float dodge;
	
	//state machine
	public StateMachine stateMachine = null;
	
	//tuning
	public float speed = 0.5f;
	public float idleDistance = 10f;
	public float attackDistance = 4f;
	
	//scirpt use varibles
	public bool canAttack = false;
	public float interval;
	public float enemyDistance;
	public float spawnerDistance;
	public int summonSourceRequireAmount;
	public Transform spawner;
	
	//gui
	public Texture2D hpBarTexture;
	private float adjustment = 2.3f;
	private float hpBarLength;
	private float hpBarWidth = 5f;
	private Vector3 worldPosition;
	private Vector3 screenPosition;
	public Camera myCamera;
	private int hpBarLeft = 70;
	//private int  barTop = 1;
	
	//public Vector3 posModifier;
	// Use this for initialization
	protected virtual void Start () 
	{
		//find camera
		myCamera = GameObject.Find("Camera").GetComponent<Camera>();
		//find the player
		player = GameObject.FindGameObjectWithTag("Player");
		minionStats = new MinionBasicStats();

		//find the state machine and set the init state
		stateMachine = GetComponent<StateMachine>();
		stateMachine.SetState(new MionionBasicIdleState(this));
		
		//access the minions summoned in DisplayMinion script
		minionsSummoned = GameObject.Find("GameMaster").GetComponent<DisplayMinion>();

		ComsumeResource();
	}



	// Update is called once per frame
	protected virtual void Update () 
	{
		CalculateHp();
		GetDistance();

		//StartCoroutine("comsumeResource");
		//print (distance);
		//update states
		//if(currentEnemy == null)
		//{
			SetToAppropriateState();
		//}
		//destroy while hp <0
		Die();
		//look at enemy
		LookAtEnemy();
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
		if(enemyDistance>attackDistance&&enemyDistance<idleDistance)
		{
			//if cant attack
			if(canAttack == false)
			{
				//update the state to move
				stateMachine.SetState(new MinionBasicMoveState(this));
				print ("moving");
			}
		}
		if(spawnerDistance>idleDistance)
		{
			ReturnToSpawner();
		}
	}	

	//change to attack state
	public void GotoAttackState()
	{
		//print (attackDistance);	
		stateMachine.SetState(new MinionBasicAttackState(this));
	}
	
	//change to idle state
	public void GotoIdleState()
	{
		if(enemyDistance>idleDistance || spawnerDistance > idleDistance)
		{
			//print ("idle");
			stateMachine.SetState(new MionionBasicIdleState(this));
			print ("idling");
		}
	}

	//function to move minion
	public virtual void Move()
	{
		Vector3 CurrentPos = transform.position;
		if(currentEnemy != null)
		{
			Vector3 moveStep = currentEnemy.transform.position - transform.position;//get direction
			moveStep.Normalize();
			
			transform.LookAt(currentEnemy.transform.position);//rotate enemy
			transform.position = CurrentPos + (moveStep * speed * Time.deltaTime);//move
			//GetDistance(player);
		}


	}

	public virtual void ReturnToSpawner()
	{
		Vector3 CurrentPos = transform.position;
		Vector3 moveStep = spawner.position - transform.position;//get direction
		moveStep.Normalize();
		
		transform.LookAt(spawner.position);//rotate enemy
		transform.position = CurrentPos + (moveStep * speed * Time.deltaTime);//move
	}

	//get distance	
	private void GetDistance()//function to get distance between minion and targetpoint
	{
		if(currentEnemy != null)
		{
			enemyDistance = Vector3.Distance(transform.position,currentEnemy.transform.position);
		}
		spawnerDistance = Vector3.Distance(transform.position,spawner.position);
	}
	
	//spawn projectile while attack
	public virtual IEnumerator Attack()
	{		
		curProjectile = Instantiate(projectile,this.transform.position,this.transform.rotation) as GameObject;
		curProjectile.GetComponent<BasicProjectile>().SetAmount(damage);
		yield return new WaitForSeconds(interval);
	}
	
	//destroy minion while hp is less than 0
	public virtual void Die()
	{
		if(minionStats.minionHealth <= 0)
		{
			//print ("die");
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
	
	private void LookAtEnemy()
	{
		if(currentEnemy != null)
		{
			transform.LookAt(currentEnemy.transform);
		}
	}
	
	
	//do nothing while idle
	public virtual void Idle()
	{
		ReturnToSpawner();
	}
	
	public abstract IEnumerator useSkill();
	
	//modify health bar length
	private void CalculateHp()
	{
		float hpPercent;
		hpPercent = minionStats.minionHealth/minionStats.minionMaxHealth;
		hpBarLength = hpPercent*50;
	}

	private void ComsumeResource()
	{
		StartCoroutine("doComsumeResource");
	}
	private IEnumerator doComsumeResource()
	{
		while(true)
		{
			if(player.GetComponent<PlayerScript>().playerStats.summonSource>0)
			{
				player.GetComponent<PlayerScript>().playerStats.SubtractSummonSource(1);
			}
			else
			{
				Destroy(gameObject);
			}
			yield return new WaitForSeconds(3.0f);
		}
	}
	
	//show health bar
	void OnGUI()
	{
		worldPosition = new Vector3(transform.position.x, transform.position.y + adjustment,transform.position.z);
		screenPosition = myCamera.WorldToScreenPoint(worldPosition);
		 GUI.DrawTexture(new Rect(screenPosition.x - hpBarLeft / 2 ,Screen.height - screenPosition.y ,hpBarLength, hpBarWidth), hpBarTexture);
	}
}
