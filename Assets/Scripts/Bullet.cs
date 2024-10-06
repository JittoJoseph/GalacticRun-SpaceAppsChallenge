using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	// Bullet movement variables
	public float speed = 10.0f;
	public float destroyTime = 5.0f; // seconds

	private void Start()
	{
		// Destroy the bullet after the specified time
		Destroy(gameObject, destroyTime);
	}

	private void Update()
	{
		// Move the bullet forward
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}