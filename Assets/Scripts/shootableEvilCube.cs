using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootableEvilCube : MonoBehaviour {

    //The box's current health point total

    public int currentHealth = 3;
    public int gunDamage = 5;
    public float fireRate = .25f;
    public float weaponRange = 10f;
    public Ray shootRay;
    public Transform target;

    private WaitForSeconds shotDuration = new WaitForSeconds(2f);
    //private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;
    

    public void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        //gunAudio = GetComponent<AudioSource>();
    }

    public void Update()
    {
        transform.LookAt(target);
        Vector3 gunPos = transform.position;
        nextFire = Time.time + fireRate;
        StartCoroutine(ShotEffect());
        RaycastHit hit;
        laserLine.SetPosition(0, gunPos);

        Debug.DrawLine(gunPos, gunPos + transform.forward * 30, Color.cyan);
        if (Time.time > nextFire)
        {
            if (Physics.Raycast(gunPos, transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);
                swimscriptv2 playerCurrentHealth = hit.collider.GetComponent<swimscriptv2>();

                if (playerCurrentHealth != null)
                    playerCurrentHealth.Damage(gunDamage);

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
    laserLine.enabled = true;
    yield return shotDuration;
    laserLine.enabled = false;

}

public void Damage(int damageAmount)

    {

        //subtract damage amount when Damage function is called

        currentHealth -= damageAmount;

        //Check if health has fallen below zero

        if (currentHealth <= 0)

        {

            //if health has fallen below zero, deactivate it 

            gameObject.SetActive(false);

        }

    }

}
