using UnityEngine;

public class shooting : MonoBehaviour {

    public float fireRate = 15f;
    public Rigidbody ammo;
    private float nextFire;
    private projectile projectile;
    [SerializeField] private bool hasHit2;

    
    void Update ()
    {
        projectile = ammo.GetComponent<projectile>();
        
        if (Input.GetButtonDown("Fire1"))
            {
             //nextFire = Time.time + 1f / fireRate;
            Shoot();
            }

	}

   void Shoot()
    {
        hasHit2 = projectile.hasHit;
        Rigidbody ammoInstance = Instantiate(ammo, transform.position, transform.rotation) as Rigidbody;
        if (hasHit2 == true)
            Debug.Log("hasHit2 is true");
        
    }
}

