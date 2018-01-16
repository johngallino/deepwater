using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootableEvilCube : MonoBehaviour {

    //The box's current health point total

    public int currentHealth = 3;
    public int gunDamage = 5;
    public float fireRate = .25f;
    public float weaponRange = 10f;
    public Ray shootRay;
    public Transform target;
	[SerializeField] private Rigidbody ammo;

    private WaitForSeconds shotDuration = new WaitForSeconds(3f);
    //private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;
	private Rigidbody ammoInstance;
    

    public void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        //gunAudio = GetComponent<AudioSource>();
    }

    public void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

		if (distanceToPlayer < 50f && ammoInstance == null)
        {
            transform.LookAt(target);
            
			ammoInstance = Instantiate(ammo) as Rigidbody;
			ammo.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
			ammo.transform.rotation = transform.rotation;


		} else if (distanceToPlayer > 50f)
		{	float _angle = Random.Range(-110, 110);
			transform.Rotate(0, _angle, 0);
		}

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


private IEnumerator ShotEffect()

{
    //gunAudio.Play();
	yield return shotDuration;
    Rigidbody ammoInstance;
	ammoInstance = Instantiate(ammo, transform.position, transform.rotation) as Rigidbody;
	ammoInstance.transform.position = transform.TransformPoint (Vector3.forward * 1.5f);
	ammoInstance.transform.rotation = transform.rotation;
    Destroy(ammoInstance, 2.0f);

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
