using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    // TODO FIX LGT BUG

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
    
    enum State { Alive, Dead, Transcending };
    [SerializeField]
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
        rocketAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
        
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

        //rocketRB.freezeRotation = false; // Rotation control back to Phyics [OLD]
        rocketRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
            return;

        switch (collision.gameObject.tag)
        {
            case "Safe":
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextScene", 1f);  // parameterise time
                break;
            case "Fatal":
                state = State.Dead;
                rocketAS.Stop();
                Invoke("LoadFirstScene", 1f);
                break;
            case "Fuel":
                print("Refueled!");
                break;
            default:
                //print("Collided with Other");
                break;
        }
    }


    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }
}
