using UnityEngine;
using System.Collections;


    public class JGSmoothFollow : MonoBehaviour
    {
        // The target we are following
        [SerializeField] private Transform target;
        [SerializeField] private Transform camtarget;

        // The distance in the x-z plane to the target
        [SerializeField] private float distance = 10.0f;

        // the height we want the camera to be above the target
        [SerializeField] private float height = 5.0f;
        [SerializeField] private float rotationDamping = 10.0f;
        [SerializeField] private float heightDamping;

        public GameObject erika;
        private ErikaHealth erikaHealthScript;
        private bool erikaIsDead;
        private Animator anim;
        

        // Use this for initialization
        void Start() {

        erikaHealthScript = erika.GetComponent<ErikaHealth>();
        anim = GetComponent<Animator>();
        }

   void Update()
    {
        erikaIsDead = erikaHealthScript.isDead;
    }

    // Update is called once per frame
    void LateUpdate()
        {
            // Early out if we don't have a target
            if (!target)
                return;

        if (erikaIsDead != true)
           {
                CameraGood();
           }
        else
            {
            StartCoroutine(DeathCam());
            }


        }

        private IEnumerator DeathCam()
        {
        DestroyObject(anim);
        transform.LookAt(Vector3.down);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y+1, transform.localPosition.z);
        yield return new WaitForSeconds(5.0f);
        Application.Quit();
        }

    void CameraGood()
    {  // Calculate the current rotation angles
        Quaternion currentRot = transform.rotation;
        Quaternion goalRot = target.rotation;
        transform.rotation = Quaternion.Slerp(currentRot, goalRot, 1.0f / rotationDamping);

        // Calculate height
        var wantedHeight = target.position.y + height;
        var currentHeight = transform.position.y;

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = target.position;
        transform.position -= currentRot * Vector3.forward * distance;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, transform.position.y + (height / 2), transform.position.z);
    }
}

