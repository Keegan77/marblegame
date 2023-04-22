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
    private void Update()
    {

    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Horizontal") * sensitivity;
        float mouseY = Input.GetAxisRaw("Vertical") * sensitivity;
        Debug.Log(mouseX);
        eulerAngleVelocity = new Vector3(mouseY, mouseX, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
