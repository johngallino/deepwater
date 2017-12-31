using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(CharacterController))]
    public class swimscript : MonoBehaviour
    {

        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] private float turnspeed;

        //private Rigidbody swimmer;

        private Camera m_Camera;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private Vector3 m_OriginalCameraPosition;


        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_MouseLook.Init(transform, m_Camera.transform);

            //swimmer = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            //RotateView();
            
/*
            if (Input.GetButton("Forward"))
            {
                swimmer.AddRelativeForce(0, speed, 0);

            }

            if (Input.GetButton("Reverse"))
            {
                swimmer.AddRelativeForce(0, -speed, 0);

            }

            if (Input.GetButton("Left"))
            {
                swimmer.AddRelativeForce(0, 0, -speed);
            }

            if (Input.GetButton("Right"))
            {
                swimmer.AddRelativeForce(0, 0, speed);
            }

 */
        }

        private void FixedUpdate()
        {
           float speed;
           GetInput(out speed);

           float rotY = Input.GetAxis("Mouse X")/30;
           gameObject.transform.Rotate(0, 0, -rotY);
        
           float rotX = Input.GetAxis("Mouse Y")/30;
           gameObject.transform.Rotate(-rotX, 0, 0);

           if(Input.GetKey("Forward"))
            {
                // transform.translate * (0, 5);
            }
            

           
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;
            
            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;

            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

            
            UpdateCameraPosition(speed);
        }

        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            newCameraPosition = m_Camera.transform.localPosition;
            newCameraPosition.y = m_OriginalCameraPosition.y;
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");
            

#if !MOBILE_INPUT
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }
        }

        /*
         private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }
        */

    }
}

