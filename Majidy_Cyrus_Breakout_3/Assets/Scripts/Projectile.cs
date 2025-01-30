using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour 
{

	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter2D (Collision2D c)
	{
		Destroy(this.gameObject);
	}
}
