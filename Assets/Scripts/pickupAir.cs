using UnityEngine.UI;
using UnityEngine;

public class pickupAir : MonoBehaviour
{

    public int airAmount = 20;
    public GameObject Erika;

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
            this.gameObject.SetActive(false);
            healthscript.CurrentAir += airAmount;
            if (healthscript.CurrentAir > healthscript.MaxAir)
                healthscript.CurrentAir = healthscript.MaxAir;
            Debug.Log("Erika's air level is now " + healthscript.CurrentAir);
        }
    }


}
