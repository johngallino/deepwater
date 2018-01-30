using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class pickupHealth : MonoBehaviour {

    public int healthAmount = 20;
    public string pickUpText = "Picked up a Medkit";
    public float secondsOnScreen = 2f;
    public Text pickupTextCanvas;

    private GameObject Erika;
    private ErikaHealth healthscript;
    

    // Use this for initialization


    void Start ()
    {
        Erika = GameObject.FindGameObjectWithTag("Player");
        healthscript = Erika.GetComponent<ErikaHealth>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        pickupTextCanvas.gameObject.SetActive(true);
        if (other.gameObject == Erika && healthscript.CurrentHealth < healthscript.MaxHealth)
        {
            Debug.Log("Erika's health was " + healthscript.CurrentHealth);
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            Destroy(this.gameObject, secondsOnScreen + 1);

            healthscript.CurrentHealth += healthAmount;
            if (healthscript.CurrentHealth > healthscript.MaxHealth)
                healthscript.CurrentHealth = healthscript.MaxHealth;
            Debug.Log("Erika's health is now " + healthscript.CurrentHealth);

            StartCoroutine(pickupText(pickUpText));
        }
    }

    private IEnumerator pickupText(string words)
    {
        Debug.Log("Coroutine started");
        pickupTextCanvas.text = words;
        yield return new WaitForSeconds(secondsOnScreen);
        Debug.Log("pickupText should be killed by now");
        pickupTextCanvas.gameObject.SetActive(false);
    }

}
