using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour {

    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weaponRange = 30f;
    public float hitForce = 100f;
    public GameObject crosshairPrefab;
    public Ray shootRay;
    public bool crosshair = true;

    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    //private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;
    private bool shotFired;

    // Use this for initialization
    void Start () {
    shotFired = false;
    Debug.Log("shotFired : " + shotFired);
    laserLine = GetComponent<LineRenderer>();
    //gunAudio = GetComponent<AudioSource>();

    if (crosshairPrefab != null && crosshair == true)
        {
         crosshairPrefab = Instantiate(crosshairPrefab);
        }
    }
	
	// Update is called once per frame
	void Update () {

        PositionCrosshair(shootRay);

        Vector3 gunPos = transform.position + new Vector3(0, 2, 0);

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)

        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            RaycastHit hit;
            laserLine.SetPosition(0, gunPos);

            if (Physics.Raycast(gunPos, transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);
                shootableEvilCube health = hit.collider.GetComponent<shootableEvilCube>();

                if (health != null)
                {
                    health.Damage(gunDamage);
                }

                if (hit.rigidbody != null)
                {
                    Debug.Log("Ray hit a rigidbody");
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
                else if (hit.collider != null)
                    Debug.Log("Ray hit a collider");
                else if (hit.collider == null && hit.rigidbody == null)
                    Debug.Log("Ray hit nothing");
            }
            else
            {
                laserLine.SetPosition(1, gunPos + (transform.forward * weaponRange));
            }
        }
        
        
    }

    private IEnumerator ShotEffect()

    {
        //gunAudio.Play();
        shotFired = true;
        //Debug.Log("Shot fired");
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
        shotFired = false;

    }

    void PositionCrosshair(Ray ray)
    {
        Vector3 raySpawn = transform.position + new Vector3(0, 2, 0);

        RaycastHit hit;
        
                if (Physics.Raycast(raySpawn, transform.forward, out hit))
            Debug.DrawLine(raySpawn, hit.point, Color.green);
            crosshairPrefab.transform.position = hit.point;
            crosshairPrefab.transform.LookAt(Camera.main.transform);
       
    }
}
