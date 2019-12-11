using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private float thrust;
    private float rotThrust;
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
        ProcessInput();
    }

    private void ProcessInput()
    {
        /// <summary>
        /// 
        /// </summary>

        //throw new NotImplementedException();
        if (Input.GetKey(KeyCode.Space))
        {
            if (!rocketAS.isPlaying)
                rocketAS.Play();
            if (thrust < maxThrust)
                thrust = thrust + 2f;
            rocketRB.AddRelativeForce(new Vector3(0f, thrust, 0f));
            
        }
        else
        {
            if (thrust != 0f)
                thrust = 0f;
            if (rocketAS.isPlaying)
                rocketAS.Stop();
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward); 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }
    }
}
