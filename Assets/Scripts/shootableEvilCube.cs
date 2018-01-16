using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootableEvilCube : MonoBehaviour {

    //The box's current health point total

    public int currentHealth = 3;
    public int gunDamage = 5;
    public float fireRate = 1f;
    public float weaponRange = 10f;
    public Ray shootRay;
    public Transform target;
    [SerializeField] private Rigidbody ammo;

    private WaitForSeconds shotDuration = new WaitForSeconds(3f);
    private float nextFire;
    private Rigidbody ammoInstance;

    public void Start()
    {
       
    }

    public void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer < 50f)
        {
            Vector3 relativePos = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = rotation;

            if (Time.time >= nextFire)
            
            shoot();



        } else if (distanceToPlayer > 50f)
            transform.Rotate(0, 0, 0);
        

        /*
        nextFire = Time.time + fireRate;
        //StartCoroutine(ShotEffect());
        RaycastHit hit;
        laserLine.SetPosition(0, transform.position);

        
        if (Time.time > nextFire)
        {
            Physics.Raycast(transform.position, transform.forward, out hit, weaponRange);

            laserLine.SetPosition(1, hit.point);
            swimscriptv2 playerCurrentHealth = hit.collider.GetComponent<swimscriptv2>();

            if (playerCurrentHealth != null && hit.collider == playerCurrentHealth)
                playerCurrentHealth.Damage(gunDamage);

        }*/
    }

    void shoot()
    {
        nextFire = Time.time + fireRate;
        ammoInstance = Instantiate(ammo) as Rigidbody;
	    ammoInstance.transform.position = transform.TransformPoint(Vector3.forward* 1.5f);
		ammoInstance.transform.rotation = transform.rotation;
        }

public void Damage(int damageAmount)

    {

        //subtract damage amount when Damage function is called

        currentHealth -= damageAmount;

        //Check if health has fallen below zero

        if (currentHealth <= 0)

        {

            //if health has fallen below zero, deactivate it 

            gameObject.SetActive(false);

        }

    }

}
