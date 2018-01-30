using UnityEngine.UI;
using UnityEngine;
using System.Collections;
public class pickupAir : MonoBehaviour
{

    public int airAmount = 20;
    public string pickUpText = "Picked up an Air Tank";
    public float secondsOnScreen = 2f;

    private ErikaHealth healthscript;
    public Text pickupTextCanvas;
    private GameObject Erika;


    // Use this for initialization


    void Start()
    {
        Erika = GameObject.FindGameObjectWithTag("Player");
        healthscript = Erika.GetComponent<ErikaHealth>();
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        pickupTextCanvas.gameObject.SetActive(true);
        if (other.gameObject == Erika && healthscript.CurrentAir < healthscript.MaxAir)
        {
            Debug.Log("Erika's air level was " + healthscript.CurrentAir);
            //makes pickup disappear but doesnt destroy it for a few seconds to allow coroutine to run
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            Destroy(this.gameObject, secondsOnScreen+1);

            healthscript.CurrentAir += airAmount;
            if (healthscript.CurrentAir > healthscript.MaxAir)
                healthscript.CurrentAir = healthscript.MaxAir;
            Debug.Log("Erika's air level is now " + healthscript.CurrentAir);
            
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
