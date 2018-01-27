using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private Rigidbody ammo;
    public int currentHealth = 3;
    public int damage = 20;
    public int sightRange = 50;
    public float speed = 2.0f;
    public float obstacleRange = 6.0f;
    public float fireRate = 1f;
    public Transform gunSpawn;
    [Range(0, 100)] public int DropProbability;
    public GameObject DropPrefab1;
    public GameObject DropPrefab2;
    private GameObject targetGameObject;
    private Transform targetTransform;
    private Animator anim;
    

    private float nextFire;
    private Rigidbody ammoInstance;
    private projectile projectilescript;
    private int damageFromEnemy;
    private Vector3 dropSpawn;

    public void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        targetGameObject = GameObject.Find("ERIKA/hitTarget");
        targetTransform = targetGameObject.transform;

    }

    public void Update()
    {
        // Check if player is within range
        float distanceToPlayer = Vector3.Distance(transform.position, targetTransform.position);

        if (distanceToPlayer > 45) // if more than 45 units away from player, swim around
        {
            anim.SetBool("isSwimming", true);
            transform.Translate(0, 0, Time.deltaTime * speed);
            /* if(Random.Range (0, 50) < 1)
             {
                 Quaternion rotationTarget = Quaternion.Euler(Random.Range(-110.0f, 110.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
                 transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, 8.0f * Time.deltaTime);
             } */

        }
        else if (distanceToPlayer < 15) // if less than 15 units from player, swim away
        {
            anim.SetBool("isSwimming", true);
            //Vector3 away = targetTransform.position - transform.position;
            transform.Translate(Vector3.back * (speed * 2) * Time.deltaTime);
        }
        else // if less than 45 units, but more than 15 units, turn towards player and shoot
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
            Debug.Log(gameObject.name + " hit with " + _collision.gameObject);
            GameObject projectile = _collision.gameObject;
            projectilescript = projectile.GetComponent<projectile>();
            damageFromEnemy = projectilescript.enemyDamage;
            DealDamage(damageFromEnemy);

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
            if(Random.Range(0, 100) < DropProbability)
            { 
            
            Debug.Log("Loot1 dropped");
            Instantiate(DropPrefab1);
            DropPrefab1.transform.position = dropSpawn;
                if(Random.Range(0,100) < DropProbability/1.5)
                {
                    Debug.Log("Loot2 dropped");
                    Instantiate(DropPrefab2);
                    DropPrefab2.transform.position = dropSpawn + new Vector3(0,5,0);
                }
            }
        }
        gameObject.SetActive(false);

    }
}



