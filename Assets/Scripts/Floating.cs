using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    private bool inWater = false;
    private Collider currentWater;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (inWater)
        {
            if (transform.position.y < currentWater.transform.position.y)
            {
                rb.AddForce(new Vector3(0, 25f * rb.mass, 0), ForceMode.Force);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            inWater = true;
            currentWater = other;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            inWater = false;
        }
    }
}
