using UnityEngine;


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

        // Use this for initialization
        void Start() { }


        // Update is called once per frame
        void LateUpdate()
        {
            // Early out if we don't have a target
            if (!target)
                return;

            // Calculate the current rotation angles
            Quaternion currentRot = transform.rotation;
            Quaternion goalRot = target.rotation;
            transform.rotation = Quaternion.Slerp(currentRot, goalRot, 1.0f/rotationDamping);

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
            transform.position = new Vector3(transform.position.x, transform.position.y + (height/2), transform.position.z);

        }

        public void UnusedShit()
        {
            // Convert the angle into a rotation
            //var currentRotation = Quaternion.Euler(currentRotationAngleX, currentRotationAngleY, 0);

            // Damp the rotation around the y-axis
            //currentRotationAngleY = Mathf.LerpAngle(currentRotationAngleY, wantedRotationAngleY, rotationDampingY * Time.deltaTime);

            // Damp the rotation around the x-axis
            //currentRotationAngleX = Mathf.LerpAngle(currentRotationAngleX, wantedRotationAngleX, rotationDampingX * Time.deltaTime);

            // Always look at the target
            //transform.LookAt(camtarget);
        }

    }

