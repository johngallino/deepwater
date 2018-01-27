using UnityEngine.UI;
using UnityEngine;

public class pickupHealth : MonoBehaviour {

    public int healthAmount = 20;
    public GameObject Erika;

    private ErikaHealth healthscript;
    
    // Use this for initialization

   
    void Start () {
        Erika = GameObject.FindGameObjectWithTag("Player");
        healthscript = Erika.GetComponent<ErikaHealth>();
        }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Erika && healthscript.CurrentHealth < healthscript.MaxHealth)
        {
            Debug.Log("Erika's health was " + healthscript.CurrentHealth);
            this.gameObject.SetActive(false);
            healthscript.CurrentHealth += healthAmount;
            if (healthscript.CurrentHealth > healthscript.MaxHealth)
                healthscript.CurrentHealth = healthscript.MaxHealth;
            Debug.Log("Erika's health is now " + healthscript.CurrentHealth);
        }
    }

    
}
