using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

	public float speed  = 5f;
	public int damage = 5;
	public float lifetime = 4.0f;

	void Start () {

		Destroy(gameObject, lifetime);

	}

	
	// Update is called once per frame
	void Update () {

		transform.Translate (0, 0, speed * Time.deltaTime);
	}




}
