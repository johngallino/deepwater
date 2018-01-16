using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

	public float speed  = 5f;
	public int damage = 5;
	public float lifetime = 4.0f;
    public bool hasHit;
    private Rigidbody rb;

	void Start () {

        hasHit = false;
		Destroy(gameObject, lifetime);
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(Vector3.forward * speed);
    }


    // Update is called once per frame
    void Update () {
        

        //transform.Translate (0, 0, speed * Time.deltaTime);

    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Projectile collided");
        hasHit = true;
        Destroy(this.gameObject);
        if (hasHit == true)
            Debug.Log("hasHit is true");
    }






}
