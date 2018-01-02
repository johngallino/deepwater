using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour {

    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;

    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    //private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;

    // Use this for initialization
    void Start () {
    
    laserLine = GetComponent<LineRenderer>();
    //gunAudio = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {

        Vector3 gunPos = transform.position + new Vector3(0, 2, 0);

        Debug.DrawLine(transform.position, transform.forward * 30, Color.green);
        Debug.DrawLine(gunPos, transform.forward * 30, Color.magenta);

        laserLine.SetPosition(0, gunPos);

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)

        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
        }

        RaycastHit hit;
        

        if (Physics.Raycast(gunPos, transform.forward, out hit, weaponRange))
        {
            laserLine.SetPosition(1, hit.point);
            if (hit.rigidbody != null)
                Debug.Log("Ray hit a rigidbody"); // this doesn't work for some reason
        }
        else
        {
            laserLine.SetPosition(1, transform.forward * weaponRange);
        }
    }

    private IEnumerator ShotEffect()

    {
        //gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
        Debug.Log("Shot fired");

    }

}
