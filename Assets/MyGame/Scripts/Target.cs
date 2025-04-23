using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
 
public class Target : MonoBehaviour
{
    public ParticleSystem targetParticleSystem;
    public AudioSource targetAudioSource;
    public UnityEvent onTargetHitEvent;
 
    // Start is called before the first frame update
    void Start()
    {
 
    }
 
    // Update is called once per frame
    void Update()
    {
         
    }
     
    // OnCollisionEnter is called when this collider/rigidbody 
    // has begun touching another rigidbody/collider.
    public IEnumerator OnCollisionEnter(Collision collider)
    {
        Debug.Log("Target was hit by: " + collider.gameObject.name);
 
        targetParticleSystem.Play();
        targetAudioSource.Play();
 
        // Disable the target's MeshCollider so it can not be hit multiple times.
        GetComponent<MeshCollider>().enabled = false;
        // Disable the target's MeshRenderer to turn it invisible
        GetComponent<MeshRenderer>().enabled = false;
     
        if (onTargetHitEvent != null)
        {
            // If there are functions subscribed to the onTargetHitEvent invoke (call) them.
            onTargetHitEvent.Invoke();
        }
         
        yield return null;
    }
}