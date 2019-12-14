using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField]
    Vector3 movementVector;

    Vector3 startingPos;

    // TODO remove later
    [SerializeField]
    [Range(0f,1f)]
    float movementFraction; // 0f for not moved, 1f for fully moved

    [SerializeField]
    private float period;

    const float tau = Mathf.PI * 2;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(tau * cycles);
        Vector3 offset = movementVector * movementFraction;
        transform.position = startingPos + offset;
    }
}
