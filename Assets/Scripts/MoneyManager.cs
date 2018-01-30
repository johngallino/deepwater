using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour {

    public int balance = 0;
    public Text moneyText;

    private string balanceString;


    // Use this for initialization
    void Start () {

        balanceString = "$" + balance.ToString();
        moneyText.text = balanceString; 
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addMoney(int moneyToAdd)
    {
        balance += moneyToAdd;
        balanceString = "$" + balance.ToString("#,#");
        moneyText.text = balanceString;
    }

    public void subtractMoney (int moneyToSubtract)
    {
        if(balance - moneyToSubtract <0)
        {
            Debug.Log("Insufficent funds");
            
        }
        else
        {
            balance -= moneyToSubtract;
            balanceString = "$" + balance.ToString();
            moneyText.text = balanceString;
        }
    }
}
