using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float rocketThrust = 1f;
    [SerializeField] float sideThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThrustParticles;
    Light engineLight;
    Rigidbody myrigidbody;
    AudioSource audioSource;


    

    void Start()
    { 
        myrigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        engineLight = GameObject.Find("EngineLight").GetComponent<Light>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
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

        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(sideThrust);
        }

        if (Input.GetKey(KeyCode.D))  //if we dont want both rotation directions to be pressed at the same time we can add else if here <--
        {
            ApplyRotation(-sideThrust);
        }
    }

    void StartThrusting()
    {
        myrigidbody.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        else if (!mainThrustParticles.isPlaying)
        {
            mainThrustParticles.Play();
        }
        else if (!engineLight.enabled)
        {   
            engineLight.enabled = true;
        }
        
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainThrustParticles.Stop();
        engineLight.enabled = false;
    }

    void ApplyRotation(float rotationThisFrame)
    {
        myrigidbody.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        myrigidbody.freezeRotation = false; //unfreezing rotation so physics can takeover
    }

}
