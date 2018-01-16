using UnityEngine.UI;
using UnityEngine;

public class healthbar : MonoBehaviour {

    public float currentHealth = 100f;
    public float totalHealth = 100f;
    public float damageAmount = 10f;

    private Scrollbar health;

	// Use this for initialization
	void Start () {
        health = GetComponent<Scrollbar>();
	}
	
	// Update is called once per frame
	void Update () {

        TakingDamage();

        health.size = currentHealth / totalHealth;
	}

    void TakingDamage()
    {
        if (Input.GetButtonDown("Fire2"))
            currentHealth -= damageAmount;
    }
}
