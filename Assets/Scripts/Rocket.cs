using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float thrust;
    private Rigidbody rocketRB;
    [SerializeField]
    private const float maxThrust = 50f;
    // Start is called before the first frame update
    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        //throw new NotImplementedException();
        if (Input.GetKey(KeyCode.Space))
        {
            if (thrust < maxThrust)
                thrust = thrust + 2f;
            rocketRB.AddRelativeForce(new Vector3(0f, thrust, 0f));
        }
        else
        {
            if (thrust != 0f)
                thrust = 0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            print("Rotating Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("Rotating Right");
        }
    }
}
