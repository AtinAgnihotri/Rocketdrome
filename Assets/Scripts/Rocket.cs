using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private AudioClip death, finish, engine;
    [SerializeField]
    private ParticleSystem deathParticle, finishParticle, engineParticle;
    private float transitionTime;

    
    enum State { Alive, Dead, Transcending };
    [SerializeField]
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
        rocketAS = GetComponent<AudioSource>();
        rocketAS.clip = engine;
        rocketAS.loop = true;
        float finishLength = finish.length;
        float deathLength = death.length;
        transitionTime = deathLength > finishLength ? deathLength : finishLength;
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

        rocketRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }

    private void Thrust()
    {
        /// <summary>
        /// 
        /// </summary>

        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();

            

        }
        else
        {
            StopThrust();
        }
    }

    private void StopThrust()
    {
        if (rocketAS.isPlaying)
            rocketAS.Stop();
        if (engineParticle.isPlaying)
            engineParticle.Stop();

        // CAN USE LATER IF GOING WITH SCALING THRUST
        //if (thrust != 0f)
        //    thrust = 0f;
    }

    private void ApplyThrust()
    {
        if (!rocketAS.isPlaying)
            rocketAS.Play();

        rocketRB.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        engineParticle.Play();
        // CAN USE LATER IF GOING WITH SCALING THRUST
        //if (thrust < maxThrust)
        //    thrust = thrust + 2f;
        //rocketRB.AddRelativeForce(new Vector3(0f, thrust, 0f));
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
                LevelComplete();
                break;
            case "Fatal":
                PlayerDied();
                break;
            case "Fuel":
                print("Refueled!");
                break;
            default:
                break;
        }
    }

    private void PlayerDied()
    {
        state = State.Dead;
        StopThrust();
        rocketAS.PlayOneShot(death);
        deathParticle.Play();
        Invoke("LoadFirstScene", transitionTime);
    }

    private void LevelComplete()
    {
        state = State.Transcending;
        StopThrust();
        rocketAS.PlayOneShot(finish);
        finishParticle.Play();
        Invoke("LoadNextScene", transitionTime);
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
