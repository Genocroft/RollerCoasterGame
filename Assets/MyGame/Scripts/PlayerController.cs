using System;
    using UnityEngine;
     
    public class PlayerController : MonoBehaviour
    {
        [Header("Camera Settings")]
        public Transform cameraTransform;
        public float mouseSensitivity = 100f;
     
        [Header("Gun Settings")]
        public Transform gunHolder;
        public GameObject gun;
     
        public Transform gunCameraTransform;
        bool holdingGun = false;
     
        private CharacterController characterController;
        private float verticalRotation = 0f;
     
        void Start()
        {
            characterController = GetComponent<CharacterController>();
     
            // Lock the cursor to the center of the screen
            Cursor.lockState = CursorLockMode.Locked;
        }
     
        void Update()
        {
            HandleCameraRotation();
            HandleGunInput();
        }
      
     
        void HandleCameraRotation()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
     
            // Rotate the player horizontally
            transform.Rotate(Vector3.up * mouseX);
     
            // Rotate the camera vertically
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
     
            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
     
        void HandleGunInput()
        {
            if (Input.GetKeyDown(KeyCode.E)) // Grab or drop the gun
            {
                Debug.Log("Grab or drop the gun");
                if (!holdingGun)
                {
                    AttemptToGrabGun();
                }
                else
                {
                    DropGun();
                }
            }
     
            if (holdingGun && Input.GetButtonDown("Fire1")) // Fire the gun
            {
                FireGun();
            }
        }
     
        void AttemptToGrabGun()
        {
            // Cast a ray from the camera to detect a gun
            if ( (gun.transform.position - transform.position).magnitude < 2){
                Debug.Log("Grabbing gun");
                holdingGun = true;
                gun.transform.SetParent(gunCameraTransform);
                gun.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
        }
     
        void DropGun()
        {
            if (!holdingGun)
            {
                gun.GetComponent<VRGun>().OnRelease();
                gun.transform.SetParent(null);
                gun = null;
            }
        }
     
        void FireGun()
        {
            if (holdingGun)
            {
                gun.GetComponent<VRGun>().FireBullet();
            }
        }
    }