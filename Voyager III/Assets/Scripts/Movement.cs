using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Movement : MonoBehaviour
{
    // Variables

    // PARAMETERS - for tuning
    [SerializeField]float thrustSpeed = 100f;
    [SerializeField]float rotationSpeed = 100f;
    [SerializeField] AudioClip mainEngine;
    
    // CACHE - to clean our code and make it more readable
    Rigidbody rb;
    AudioSource audioSource;

    // Start and Update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource =  GetComponent<AudioSource>();

    }
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    // Private Methods
    void ProcessThrust()
    {
        // If we press space, add an upwards force, make sure audio isn't playing, if it's not, play it. When we let go of space, 
        //stop playing
        if (Input.GetKey(KeyCode.Space))
        {
           rb.AddRelativeForce(Vector3.up*Time.deltaTime*thrustSpeed);
           if(!audioSource.isPlaying)
           {
               audioSource.PlayOneShot(mainEngine);
           }
        }
        else
        {
            audioSource.Stop();
        }

    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freezing the Rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing the rotation//

    }
}
