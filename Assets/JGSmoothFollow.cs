using UnityEngine;

namespace UnityStandardAssets.Utility
{
    public class JGSmoothFollow : MonoBehaviour
    {
        // The target we are following
        [SerializeField] private Transform target;
        [SerializeField] private Transform camtarget;

        // The distance in the x-z plane to the target
        [SerializeField] private float distance = 10.0f;

        // the height we want the camera to be above the target
        [SerializeField] private float height = 5.0f;

        [SerializeField] private float rotationDampingY = 1.0f;

        [SerializeField] private float rotationDampingX = 1.0f;

        [SerializeField] private float heightDamping;

        // Use this for initialization
        void Start() { }

        // Update is called once per frame
        void LateUpdate()
        {
            // Early out if we don't have a target
            if (!target)
                return;

            // Calculate the current rotation angles for y axis
            var wantedRotationAngleY = target.eulerAngles.y; // player's y rotation
            var currentRotationAngleY = transform.eulerAngles.y; // camera's y rotation

            // Calculate the current rotation angles for x axis
            var wantedRotationAngleX = target.eulerAngles.x; // player's x rotation
            var currentRotationAngleX = transform.eulerAngles.x; // camera's x rotation

            // Calculate height
            var wantedHeight = target.position.y + height;
            var currentHeight = transform.position.y;

            // Damp the rotation around the y-axis
           currentRotationAngleY = Mathf.LerpAngle(currentRotationAngleY, wantedRotationAngleY, rotationDampingY * Time.deltaTime);
            
            // Damp the rotation around the x-axis
            currentRotationAngleX = Mathf.LerpAngle(currentRotationAngleX, wantedRotationAngleX, rotationDampingX * Time.deltaTime);

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            var currentRotation = Quaternion.Euler(currentRotationAngleX, currentRotationAngleY, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * distance;

            // Set the height of the camera
            transform.position = new Vector3(transform.position.x, transform.position.y + (height/2), transform.position.z);

            // Always look at the target
            transform.LookAt(camtarget);
        }
    }
}