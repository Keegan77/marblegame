using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    private float x, y;
    [SerializeField] float sensitivity;
    Rigidbody rb;
    Vector3 eulerAngleVelocity;

    private void FixedUpdate()
    {
        Rotate();
        
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Rotate()
    {
        float mouseX = Input.GetAxis("Horizontal") * sensitivity;
        float mouseY = Input.GetAxis("Vertical") * sensitivity;
        Debug.Log(mouseX);
        eulerAngleVelocity = new Vector3(mouseX, mouseY, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
