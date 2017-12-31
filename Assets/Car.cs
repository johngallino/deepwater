using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {
	private Rigidbody carRigid;
	public int speed;
	public int rotateSpeed;

	// Use this for initialization
	void Start () {

		carRigid = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {


	}


	void FixedUpdate(){

		if (Input.GetButton ("Forward")) {
          carRigid.AddRelativeForce(0, speed, 0);
           
        }

		if (Input.GetButton ("Reverse")) {
            carRigid.AddRelativeForce(0, -speed, 0);
            
        }

		if (Input.GetButton ("Left")) {
            carRigid.AddRelativeForce(0, 0, -speed);
        }

		if (Input.GetButton ("Right")) {
            carRigid.AddRelativeForce(0, 0, speed);
        }



	}



}﻿