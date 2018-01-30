using UnityEngine.UI;
using UnityEngine;
using System.Collections;
public class pickupAir : MonoBehaviour
{

    public int airAmount = 20;
    public GameObject Erika;
    public Text myText;
    public string newString;
    private ErikaHealth healthscript;

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
        if (other.gameObject == Erika && healthscript.CurrentAir < healthscript.MaxAir)
        {
            Debug.Log("Erika's air level was " + healthscript.CurrentAir);
            StartCoroutine(SetText(newString));
            Destroy(this.gameObject);
            healthscript.CurrentAir += airAmount;
            if (healthscript.CurrentAir > healthscript.MaxAir)
                healthscript.CurrentAir = healthscript.MaxAir;
            Debug.Log("Erika's air level is now " + healthscript.CurrentAir);
        }
    }

    IEnumerator SetText(string newString)
    {
        myText.text = newString;
        yield return new WaitForSeconds(3f);
        myText.text = null;
    }
}
