using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class swimscriptv2 : MonoBehaviour
    {

        public float walkSpeed = 5.0f;
        public float runSpeed = 30.0f;
        public float mouseSensitivity = 5.0f;
        public float clampXAngle = 35.0f;
        public bool lockCursor = true;
       


        private float rotY = 0.0f; // rotation around the up/y axis
        private float rotX = 0.0f; // rotation around the right/x axis
        private float movementSpeed;
        private bool m_cursorIsLocked;


    // Use this for initialization
    void Start()
        {
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
            float movementSpeed = 3.0f;

        
        UpdateCursorLock();

        movementSpeed = walkSpeed;

        if (Input.GetButton("Sprint"))
        {
            movementSpeed = runSpeed;
        }

        //this is movement
        transform.Translate(horizontal * movementSpeed * Time.deltaTime, updown * movementSpeed * Time.deltaTime, forwardback * movementSpeed * Time.deltaTime);

            //transform.position += direction * movementSpeed * Time.deltaTime;

            //this is rotation
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            rotY += mouseX * mouseSensitivity * Time.deltaTime; // left and right
            rotX += mouseY * mouseSensitivity * Time.deltaTime; // up and down

            rotX = Mathf.Clamp(rotX, -clampXAngle, clampXAngle); // up and down clamp

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
        
    }

    public void UpdateCursorLock()
    {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


}

