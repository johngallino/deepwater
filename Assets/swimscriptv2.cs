using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class swimscriptv2 : MonoBehaviour
    {

        public float movementSpeed = 3.0f;
        public float mouseSensitivity = 5.0f;
        public float clampAngle = 35.0f;

        private float rotY = 0.0f; // rotation around the up/y axis
        private float rotX = 0.0f; // rotation around the right/x axis
    
        Quaternion swimrot = Quaternion.Euler(90, 0, 0);


    // Use this for initialization
    void Start()
        {

        // starts player in swimming position
        //transform.localRotation = swimrot;

        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

    }

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxis("Strafe"); // slide on x axis
            float forwardback = Input.GetAxis("ForwardBack"); // slide on z axis
            float updown = Input.GetAxis("UpDown"); // slide on y axis
         
            
            //this is movement
            Vector3 direction = new Vector3(horizontal, updown, forwardback);
            transform.Translate(horizontal * movementSpeed * Time.deltaTime, 0, forwardback * movementSpeed * Time.deltaTime);

            //transform.position += direction * movementSpeed * Time.deltaTime;

            //this is rotation
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            rotY += mouseX * mouseSensitivity * Time.deltaTime; // left and right
            rotX += mouseY * mouseSensitivity * Time.deltaTime; // up and down

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle); // up and down clamp

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left click.");
            
        }

    }

       
    }

