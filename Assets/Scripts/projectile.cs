using UnityEngine;

public class projectile : MonoBehaviour {

	public float speed  = 5f;
	public float lifetime = 4.0f;
   
    public int enemyDamage = 5;
    private Rigidbody rb;

	void Start () {

        Destroy(gameObject, lifetime);
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(Vector3.forward * speed);
    }

    void Update () {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            Debug.Log("Projectile hit " + collision.gameObject.name + " doing " + enemyDamage + " damage");
            Destroy(this.gameObject, .25f);
        }
        
    }






}
