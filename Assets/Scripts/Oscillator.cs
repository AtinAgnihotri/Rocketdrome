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

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = movementVector * movementFraction;
        transform.position = startingPos + offset;
    }
}
