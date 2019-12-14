using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField]
    Vector3 movementVector;

    Vector3 startingPos = new Vector3(10f,10f,10f);

    // TODO remove later
    [SerializeField]
    [Range(0f,1f)]
    float movementFraction; // 0f for not moved, 1f for fully moved

    [SerializeField]
    private float period = 2f;

    const float tau = Mathf.PI * 2;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Oscillate();
    }

    private void Oscillate()
    {
        //if(period <= Mathf.Epsilon) { return; }  CAN ALSO LOOK LIKE THIS

        if (period > Mathf.Epsilon) //protect against divide by 0
        {
            float cycles = Time.time / period;  // Grows from 0
            float rawSinWave = Mathf.Sin(tau * cycles); // Goes from -1 to -1
            movementFraction = 0.5f + rawSinWave / 2f;

            Vector3 offset = movementVector * movementFraction;
            transform.position = startingPos + offset;
        }
    }
}
