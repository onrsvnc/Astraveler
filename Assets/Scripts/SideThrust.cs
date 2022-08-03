using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideThrust : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] ParticleSystem A1thrust;
    [SerializeField] ParticleSystem A2thrust;
    [SerializeField] ParticleSystem D1thrust;
    [SerializeField] ParticleSystem D2thrust;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        SideThrustSFXPlayer();
        AThrustParticle();
        DThrustParticle();
    }

    void SideThrustSFXPlayer()
    {

        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        else
        {
            audioSource.Stop();
        }

    }

    void AThrustParticle()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if(!A1thrust.isPlaying)
            {
                A1thrust.Play();
                A2thrust.Play();
            }
        }
        else
        {
            A1thrust.Stop();
            A2thrust.Stop();
        }


    }

    void DThrustParticle()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if(!D1thrust.isPlaying)
            {
                D1thrust.Play();
                D2thrust.Play();
            }
        }
        else
        {
            D1thrust.Stop();
            D2thrust.Stop();
        }


    }




}
