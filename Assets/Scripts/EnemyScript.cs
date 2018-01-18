using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public int currentHealth = 3;
    public int damage = 20;
    public int sightRange = 50;
    public float fireRate = 1f;
    private GameObject targetGameObject;
    private Transform targetTransform;
    [SerializeField] private Rigidbody ammo;

    private float nextFire;
    private Rigidbody ammoInstance;
    private projectile projectilescript;
    private int damageFromEnemy;
    
    public void Start()
    {
        targetGameObject = GameObject.Find("ERIKA/hitTarget");
        targetTransform = targetGameObject.transform;
               
    }

    public void Update()
    {
        // Check if player is within range
        float distanceToPlayer = Vector3.Distance(transform.position, targetTransform.position);

        if (distanceToPlayer < sightRange)
        {
            Vector3 relativePos = targetTransform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = rotation;

            if (Time.time >= nextFire)
           
            shoot();
            
        } else if (distanceToPlayer > 50f)
            transform.Rotate(0, 0, 0);
        
        
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject.tag == "MyProjectile")
        {
            Debug.Log(gameObject + " hit with " + _collision.gameObject);
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
        ammoInstance.transform.position = transform.TransformPoint(Vector3.forward* 1.5f);
		ammoInstance.transform.rotation = transform.rotation;
        
    }

public void DealDamage(int damageAmount)

    {

        currentHealth -= damageAmount;

        if (currentHealth <= 0)

        {
            Debug.Log(gameObject + " is dead");
            gameObject.SetActive(false);

        }

    }


}
