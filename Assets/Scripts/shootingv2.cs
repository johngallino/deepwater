using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingv2 : MonoBehaviour {

    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weaponRange = 30f;
    public float hitForce = 100f;
    public Rigidbody ammo;
    public GameObject crosshairPrefab;
    public bool crosshairOn = true;
    public Transform gunBarrel;


    private float nextFire;
       
    // Use this for initialization
    void Start () {
     //gunAudio = GetComponent<AudioSource>();

    if (crosshairPrefab != null && crosshairOn == true)
        {
			

			Instantiate (crosshairPrefab, gunBarrel);
			Debug.Log ("Crosshair instnatiated");
        }
    }

	// Update is called once per frame
	void Update () {

		Vector3 raySpawn = gunBarrel.transform.position;


		Ray ray = new Ray(gunBarrel.position, gunBarrel.transform.forward);
		Debug.DrawLine(gunBarrel.position, gunBarrel.transform.forward * 1000, Color.green);

		RaycastHit hit;

		if (Physics.Raycast(ray, out hit))
		{ 
			Debug.Log ("Hitting something");
			crosshairPrefab.transform.position = hit.point;
			crosshairPrefab.transform.LookAt(Camera.main.transform);
		}

		if (hit.rigidbody != null)
		Debug.Log("Ray hit a rigidbody");
		else if (hit.collider != null)
			Debug.Log("Ray hit a collider");
		
	

        if (Input.GetButtonDown("Fire1"))
            //if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
            {
            Rigidbody ammoInstance;
            //nextFire = Time.time + fireRate;

            ammoInstance = Instantiate(ammo, gunBarrel.position, gunBarrel.rotation) as Rigidbody;
            ammoInstance.AddForce(gunBarrel.forward * 5000);
            
        	}
        
		if (Input.GetButtonDown ("Fire2")) 
		{
			GameObject clone = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			clone.transform.position = gunBarrel.position;

    	}

	}
}

