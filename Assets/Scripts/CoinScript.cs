using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    public int value = 20;
    public GameObject Erika;
    public string newString;
    
    

    // Use this for initialization
    void Start () {
        Erika = GameObject.Find("ERIKA");



    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Erika)
        {
            Debug.Log("Picked up a gold coin");
            Erika.GetComponent<MoneyManager>().addMoney(value);
            Destroy(gameObject);
            
            
        }
    }
}
