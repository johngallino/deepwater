using UnityEngine.UI;
using UnityEngine;
using System.Collections;
public class pickupAir : MonoBehaviour
{

    public int airAmount = 20;
    public GameObject Erika;
    public GameObject pickupTextGO;
    public Text myText;
    public string newString;
    public float secondsOnScreen = 3;
    private ErikaHealth healthscript;
    private bool isTaken = false;

    // Use this for initialization


    void Start()
    {
        Erika = GameObject.FindGameObjectWithTag("Player");
        healthscript = Erika.GetComponent<ErikaHealth>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (secondsOnScreen > 0 && isTaken == true)
        {
            secondsOnScreen -= Time.deltaTime;
            myText.gameObject.SetActive(true);
            myText.text = "Picked up small air tank";

        }
        else if (isTaken == false|| secondsOnScreen == 0)
        {
            myText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Erika && healthscript.CurrentAir < healthscript.MaxAir)
        {
            Debug.Log("Erika's air level was " + healthscript.CurrentAir);
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            Destroy(this.gameObject, secondsOnScreen);
            healthscript.CurrentAir += airAmount;
            if (healthscript.CurrentAir > healthscript.MaxAir)
                healthscript.CurrentAir = healthscript.MaxAir;
            Debug.Log("Erika's air level is now " + healthscript.CurrentAir);
            isTaken = true;
        }
    }

    
}
