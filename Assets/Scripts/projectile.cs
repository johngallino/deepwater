using UnityEngine;

public class projectile : MonoBehaviour {

	public float speed  = 5f;
	public float lifetime = 4.0f;
    public bool hasHit;
    private Rigidbody rb;

	void Start () {

        hasHit = false;
        Destroy(gameObject, lifetime);
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(Vector3.forward * speed);
    }

    void Update () {
        Debug.Log("hasHit is " + hasHit);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Projectile collided");
        hasHit = true;
        Destroy(this.gameObject); 
    }






}
