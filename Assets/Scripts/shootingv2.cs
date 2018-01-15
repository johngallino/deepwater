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

    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    //private AudioSource gunAudio;

    private float nextFire;
    private Ray ray;
       
    // Use this for initialization
    void Start () {
     //gunAudio = GetComponent<AudioSource>();

    if (crosshairPrefab != null && crosshairOn == true)
        {
         crosshairPrefab = Instantiate(crosshairPrefab);
        }
    }
	
	// Update is called once per frame
	void Update () {

        crosshairPrefab.transform.position = Input.mousePosition;



        if (Input.GetButtonDown("Fire1"))
            //if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
            {
            Rigidbody ammoInstance;
            //nextFire = Time.time + fireRate;

            ammoInstance = Instantiate(ammo, gunBarrel.position, gunBarrel.rotation) as Rigidbody;
            ammoInstance.AddForce(gunBarrel.forward * 5000);
            
        }
        
        
    }

    

    void PositionCrosshair(Ray ray)
    {
        Vector3 raySpawn = gunBarrel.forward;
        

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(raySpawn, transform.forward, Color.green);


        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        { 
        
        crosshairPrefab.transform.position = hit.point;
        crosshairPrefab.transform.LookAt(Camera.main.transform);
        }
    }
}
