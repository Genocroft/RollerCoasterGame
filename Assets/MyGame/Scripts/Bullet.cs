using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Bullet : MonoBehaviour
{
    public ParticleSystem explosionParticleSystem;
    public AudioSource explosionAudioSource;
 
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
        Debug.Log("Bullet HIT!!!");
 
        explosionParticleSystem.Play();
        explosionAudioSource.Play();
 
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
 
        yield return null;
    }
}