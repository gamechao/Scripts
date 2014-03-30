using UnityEngine;
using System.Collections;

public abstract class BasicProjectile : MonoBehaviour 
{
	//owner
	//public GameObject projectileOwner;
	public float damageReduceMultiplier;
	
	//player information
	public PlayerReplicationInfo curPlayerStats;
	public PlayerScript player;
	
	//tuning values in editor
	protected int amount;//damage amount
	public float speed;//move speed 
	public float lifeTime;//the life time of the projectile
	public bool isEnabled;//use this to make projectile only damage once
	
	// Use this for initialization
	protected virtual void Start () 
	{
		//make the projectile enabled so we can do damage
		isEnabled = true;
		//access the player information
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
		curPlayerStats = player.playerStats;
		//kill the object after lifetime
		StartCoroutine( selfDestroy(lifeTime));
	}
	
	// Update is called once per frame
	void Update () 
	{
		Move();
	}
	
	//move projectile forward
	public virtual void Move()
	{
		transform.Translate(Vector3.forward *speed *Time.deltaTime);
	}
	
	//kill object after lifetime
	public virtual IEnumerator selfDestroy(float time)
	{
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
	
	public void SetAmount(int damage)
	{
		amount = damage;
	}
	
	//damage the collider
	protected abstract  void OnTriggerEnter(Collider other);
}
