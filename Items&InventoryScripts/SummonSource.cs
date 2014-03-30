using UnityEngine;
using System.Collections;

public class SummonSource : Item 
{
	public int amount;//the amount will be add after use
	
	protected override void Start()
	{
		base.Start();
	}
	
	protected override void OnMouseDown()
	{
		curPlayerStats.AddSource(amount);//modify the amount of summon source
		Destroy(gameObject);
	}
}
