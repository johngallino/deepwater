using UnityEngine;


public class Flock : MonoBehaviour {


    public float speed = 4f;
    public float obstacleRange = 2.0f;
    float rotationSpeed = 4.0f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    float neighborDistance = 4.0f;
    GameObject fishmanager;

    bool turning = false;

    // Use this for initialization
    void Start ()
    {
        fishmanager = GameObject.Find("FishManager");
        speed = Random.Range(3.0f, 4.0f);
	}
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(transform.position, fishmanager.transform.position)  >= GlobalFlock.tankSize)
        {
            turning = true;
        }
        else
            turning = false;

        if (turning)
        {
            Vector3 direction = fishmanager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            speed = Random.Range(3.0f, 4.0f);

        }
        else
        {

            if (Random.Range(0, 5) < 1)
                ApplyRules();
        }
        transform.Translate(0, 0, Time.deltaTime * speed);

        // The following scans and avoids obstacles in the scene
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            if (hit.distance < obstacleRange)
                {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
                }
            
        }
		
	}

    void ApplyRules()
    {
        GameObject[] gos;
        gos = GlobalFlock.allfish;

        GlobalFlock globalflock;
        globalflock = FindObjectOfType<GlobalFlock>();

        Vector3 vcenter = globalflock.transform.position;
        Vector3 vavoid = globalflock.transform.position;
        float gSpeed = 0.1f;

        Vector3 goalPos = GlobalFlock.goalPos;

        float dist;

        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if (dist <= neighborDistance)
                {
                    vcenter += go.transform.position;
                    groupSize++;

                    if (dist < 1.0f)
                    {
                        vavoid += (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;

                }
            }
        }

        if (groupSize > 0)
        {
            vcenter = vcenter / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcenter + vavoid) - transform.position;
            if (direction != fishmanager.transform.position)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                                                        rotationSpeed * Time.deltaTime);
        }
    }

    
}
