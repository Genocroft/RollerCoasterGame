using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class VRGun: MonoBehaviour
{
    public Transform gunSnapTransform;
    public Transform bulletSpawnTransform;
    public GameObject bulletPrefab;
    public ParticleSystem gunFireParticleSystem;
    public AudioSource gunFireAudioSource;
    public Collider[] gunColliders;
     
    public float fireSpeed = 125.0f;
 
    private Vector3 gunVelocity;
    private Vector3 previousPosition;
    private bool fireFlag = false;
 
    // Start is called before the first frame update
    void Start()
    {
 
    }
 
    // Update is called once per frame
    void Update()
    { 
        // Velocity is distance between current position and previous position divided by time.
        gunVelocity = (bulletSpawnTransform.position - previousPosition) / Time.deltaTime;
 
        // Store the position at the end of the Update() function for the next frame.
        previousPosition = bulletSpawnTransform.position;
    }

    // FixedUpdate is called every fixed frame-rate frame
    [System.Obsolete]
    void FixedUpdate()
    {
        // If the fireFlag boolean was set to true during Update()
        // call the Fire() function and set the fireFlag back to false.
        if(fireFlag == true)
        {
            fireFlag = false;
            Fire();
        }
    }
 
    // Called by Gun.XRGrabInteractable.InteractableEvents.Activated, when the gun is fired.
    public void FireBullet()
    {
        // Set the boolean fireFlag variable to true so that in the next
        // FixedUpdate loop iteration the Fire() function can be called. 
        fireFlag = true;
    }

    [System.Obsolete]
    public void Fire()
    {
        Debug.Log("Gun is Fired!!!");
         
        // Play the gunFireParticleSystem and the gunFireAudioSource
        gunFireParticleSystem.Play();
        gunFireAudioSource.Play();
 
        // Spawn/Instantiate a clone of the bulletPrefab and store 
        // a reference to it in the GameObject spawnedBullet variable.
        GameObject spawnedBullet = Instantiate(bulletPrefab);
 
        // Position the spawnedBullet at the tip of the gun at the bulletSpawnTransform.position.
        spawnedBullet.transform.position = bulletSpawnTransform.position;
        // Rotate the spawnedBullet in the direction of the bulletSpawnTransform
        spawnedBullet.transform.rotation = bulletSpawnTransform.rotation;
 
        // Get the spawnedBullet's Rigidbody component and set its velocity
        // to the velocity of the gun plus the forward direction 
        // of the bullet multiplied by the firespeed.
        spawnedBullet.GetComponent<Rigidbody>().velocity = gunVelocity + (spawnedBullet.transform.forward * fireSpeed);
         
        // Destroy the spawned bullet after five seconds.
        Destroy(spawnedBullet,5);
 
        // Get the SphereCollider component on the spawnedBullet and 
        // store a reference to it in the SphereCollider spawnedBulletCollider variable.
        SphereCollider spawnedBulletCollider = spawnedBullet.GetComponent<SphereCollider>();
 
        // Loop trough the gunColliders array and set IgnoreCollision 
        // between the gunColliders and the spawnedBulletCollider to true.
        for(int i=0; i<gunColliders.Length; i++)
        {
            Physics.IgnoreCollision(spawnedBulletCollider, gunColliders[i], true);
        }
    }
 
    // Called by Gun.XRGrabInteractable.InteractableEvents.FirstSelectEntered, when the gun is grabbed.
    public void OnGrab()
    {
        Debug.Log("Gun is Grabbed!!!");
    }
 
    // Called by Gun.XRGrabInteractable.Interactable Events.LastSelectExited, when the gun is released.
    public void OnRelease()
    {
        Debug.Log("Gun is Dropped!!!");
 
        transform.position = gunSnapTransform.position;
        transform.rotation = gunSnapTransform.rotation;
 
        GetComponent<Rigidbody>().isKinematic = true;
    }
}