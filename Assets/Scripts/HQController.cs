using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQController : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		// Check if the collision is with the enemy
		if (other.gameObject.name == "Enemy(Clone)")
		{
			Debug.Log("Enemy Hit me");
			DestroyImmediate(other.gameObject);
		}
	}
}
