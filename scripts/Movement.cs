using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust=1000f;
    [SerializeField] float mainRotate=1000f;
    [SerializeField] AudioClip mainEngine ;  
    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            StartRotation();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            StopRotation();
        }
    }
    void ApplyRotation(float RotationThrust)
    {
        rb.freezeRotation= true;
        transform.Rotate(Vector3.forward*RotationThrust*Time.deltaTime);
        rb.freezeRotation= false;
    }
    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up*mainThrust*Time.deltaTime);
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if(!mainThrusterParticles.isPlaying)
            {
                mainThrusterParticles.Play();
            }  
    }
    void StopThrusting()
    {
        audioSource.Stop();
        mainThrusterParticles.Stop();
    }
    void StartRotation()
    {
        ApplyRotation(mainRotate);
             if(!leftThrusterParticles.isPlaying)
            {
                leftThrusterParticles.Play();
                rightThrusterParticles.Stop();
            }
    }
    void StopRotation()
    {
        ApplyRotation(-mainRotate);
            if(!rightThrusterParticles.isPlaying)
            {
                rightThrusterParticles.Play();
                leftThrusterParticles.Stop();
            }
    }
}
