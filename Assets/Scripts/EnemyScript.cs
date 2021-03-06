﻿using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private Rigidbody ammo;
    public int currentHealth = 3;
    public int damage = 20;
    public int sightRange = 50;
    public float patrolRadius = 45f;
    public float speed = 2.0f;
    public float fireRate = 1f;
    public Transform gunSpawn;
    [Range(0, 100)] public int DropProbability;
    public GameObject DropPrefab1;
    public GameObject DropPrefab2;

    private GameObject targetGameObject;
    private Transform targetTransform;
    private Animator anim;
    private bool isHit = false;
    private bool turning = false;
    private float obstacleRange = 6.0f;
    private float nextFire;
    private Rigidbody ammoInstance;
    private projectile projectilescript;
    private int damageFromEnemy;
    private Vector3 dropSpawn;
    private float distanceToPlayer;
    private Vector3 spawnPoint;

    public void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        targetGameObject = GameObject.Find("ERIKA/hitTarget");
        targetTransform = targetGameObject.transform;
        spawnPoint = transform.position;

    }

    public void Update()
    {
        // Check if player is within range
        distanceToPlayer = Vector3.Distance(transform.position, targetTransform.position);

        if (distanceToPlayer > 45) // if more than 45 units away from player, swim around
        {
            if (isHit != true)
            { // Enemy will be in this state most of the time - patrolling with no player to shoot
                anim.SetBool("isSwimming", true);
                transform.Translate(0, 0, Time.deltaTime * speed);

                if (Vector3.Distance(transform.position, spawnPoint) >= patrolRadius)
                {
                    turning = true;
                }
                else
                    turning = false;

                if (turning)
                {
                    Vector3 direction = spawnPoint - transform.position;
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(direction), 5.0f * Time.deltaTime);
                    
                }

            }
            else
            {
                transform.LookAt(targetTransform);
                transform.Translate(0, 0, Time.deltaTime * speed * 1.5f);
            }
        }
        else if (distanceToPlayer < 15) // if less than 15 units from player, swim away
        {
            anim.SetBool("isSwimming", true);
            //Vector3 away = targetTransform.position - transform.position;
            transform.Translate(Vector3.back * (speed * 2) * Time.deltaTime);
        }
        else // if less than 45 units, but more than 15 units, STOP swimming, turn towards player and shoot
        {
            anim.SetBool("isSwimming", false);
            Vector3 relativePos = targetTransform.position - transform.position;
            Quaternion rotationTarget = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, 8.0f * Time.deltaTime);

            if (Time.time >= nextFire)

                shoot();
        }


        // Scans and avoids obstacles in the scene
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            if (hit.distance < obstacleRange)
            {
                //float angle = Random.Range(-110, 110);
                //transform.Rotate(0, angle, 0);
                Vector3 direction = transform.position - hit.point;

                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction), 4.0f * Time.deltaTime);
            }

        }

        dropSpawn = this.transform.position;

    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.tag == "MyProjectile")
        {
            Vector3 relativePos = targetTransform.position - transform.position;
            Quaternion rotationTarget = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, 8.0f * Time.deltaTime);
            Debug.Log(gameObject.name + " hit with " + _collision.gameObject);
            GameObject projectile = _collision.gameObject;
            projectilescript = projectile.GetComponent<projectile>();
            damageFromEnemy = projectilescript.enemyDamage;
            DealDamage(damageFromEnemy);
            isHit = true;
            
        }
    }

    void shoot()
    {
        nextFire = Time.time + fireRate;
        ammoInstance = Instantiate(ammo) as Rigidbody;
        ammo.tag = "Projectile";
        ammo.GetComponent<projectile>().enemyDamage = damage;
        ammoInstance.transform.position = gunSpawn.transform.position;
        ammoInstance.transform.LookAt(targetTransform);

    }

    public void DealDamage(int damageAmount)

    {

        currentHealth -= damageAmount;

        if (currentHealth <= 0)

        {
            currentHealth = 0;
            DropLoot();
            
        }

    }


    public void DropLoot()
    {

        if (DropPrefab1 != null && DropPrefab2 != null)
        {
            Debug.Log("Dropping loot");
            if (Random.Range(0, 100) < DropProbability)
            {

                Debug.Log("Loot1 dropped");
                GameObject go = (GameObject)Instantiate(DropPrefab1);

                go.transform.position = this.transform.position;
                if (Random.Range(0, 100) < DropProbability / 1.5)
                {
                    Debug.Log("Loot2 dropped");
                    GameObject go2 = (GameObject)Instantiate(DropPrefab2);
                    go2.transform.position = this.transform.position + new Vector3(0, 5, 0);
                }
            }
        }
        gameObject.SetActive(false);

    }
}



