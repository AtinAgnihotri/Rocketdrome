using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private float thrust = 1f;
    [SerializeField]
    private float rotThrust = 100f;
    private Rigidbody rocketRB;
    private AudioSource rocketAS;
    [SerializeField]
    private const float maxThrust = 50f;
    // Start is called before the first frame update
    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
        rocketAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void Rotate()
    {
        /// <summary>
        /// 
        /// </summary>

        rocketRB.freezeRotation = true; // Rotation w/o affecting Phyics
        float rotMultiplier = rotThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotMultiplier);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotMultiplier);
        }

        rocketRB.freezeRotation = false; // Rotation control back to Phyics
    }

    private void Thrust()
    {
        /// <summary>
        /// 
        /// </summary>

        if (Input.GetKey(KeyCode.Space))
        {
            if (!rocketAS.isPlaying)
                rocketAS.Play();
   
            rocketRB.AddRelativeForce(Vector3.up * thrust);

            // CAN USE LATER IF GOING WITH SCALING THRUST
            //if (thrust < maxThrust)
            //    thrust = thrust + 2f;
            //rocketRB.AddRelativeForce(new Vector3(0f, thrust, 0f));

        }
        else
        {
            if (rocketAS.isPlaying)
                rocketAS.Stop();

            // CAN USE LATER IF GOING WITH SCALING THRUST
            //if (thrust != 0f)
            //    thrust = 0f;
        }
    }
}
