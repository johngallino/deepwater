using UnityEngine;

public class shooting : MonoBehaviour {

    public float fireRate = 15f;
    public Rigidbody ammo;
    public int damage = 5;
    private float nextFire;
    private projectile projectile;
       
    
    void Update ()
    {
        
        
        if (Input.GetButtonDown("Fire1"))
            {
             //nextFire = Time.time + 1f / fireRate;
            Shoot();
            }

	}

   void Shoot()
    {
        Rigidbody ammoInstance = Instantiate(ammo, transform.position, transform.rotation) as Rigidbody;
        ammo.tag = "MyProjectile";
        ammo.GetComponent<projectile>().enemyDamage = damage;
                        
    }
}

