using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour {

    public GameObject fishPrefab;
    public int SwimArea = 5;
   
    public static int tankSize;
    static int numFish = 15;
    public static GameObject[] allfish = new GameObject[numFish];

    public static Vector3 goalPos = Vector3.zero;

	// Use this for initialization
	void Start ()
    {
        tankSize = SwimArea;

		for(int i = 0; i < numFish; i++) // populate array with numFish ammount of fish
        {
            Vector3 pos = new Vector3(Random.Range(transform.position.x-tankSize, transform.position.x+tankSize),
                                      Random.Range(transform.position.y - tankSize, transform.position.y+tankSize),
                                      Random.Range(transform.position.z - tankSize, transform.position.z+ tankSize));
            allfish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if(Random.Range(0,10000) < 50) // Move the goal position every 200 frames or so
        {
            goalPos = new Vector3(Random.Range(transform.position.x - tankSize, transform.position.x + tankSize),
                                      Random.Range(transform.position.y - tankSize, transform.position.y + tankSize),
                                      Random.Range(transform.position.z - tankSize, transform.position.z + tankSize));
            
        }
		
	}
}
