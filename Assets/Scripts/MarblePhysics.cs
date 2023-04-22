using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarblePhysics : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * Time.deltaTime * 10);
    }
}
