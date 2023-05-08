using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingLeftAndRight : MonoBehaviour
{
    [SerializeField] float ballSpeed;
    [SerializeField] float ballDistance;
    [SerializeField] bool leftAndRight;
    [SerializeField] bool upAndDown;
    [SerializeField] bool left, right, up, down;
    private void FixedUpdate()
    {
        if (leftAndRight)
        {
            if (left)
            {
                transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time * ballSpeed) * ballDistance - 90);
            }
            if (right)
            {
                transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time * ballSpeed) * ballDistance + 90);
            }
            if (up)
            {
                transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time * ballSpeed) * ballDistance - 180);
            }
            if (down)
            {
                transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time * ballSpeed) * ballDistance);
            }
            
        }
        if (upAndDown)
        {
            if (left)
            {
                transform.eulerAngles = new Vector3(0, Mathf.Sin(Time.time * ballSpeed) * ballDistance, -90);
            }
            if (right)
            {
                transform.eulerAngles = new Vector3(0, Mathf.Sin(Time.time * ballSpeed) * ballDistance, 90);
            }
            if (up)
            {
                transform.eulerAngles = new Vector3(Mathf.Sin(Time.time * ballSpeed) * ballDistance, 0, -180); ;
            }
            if (down)
            {
                transform.eulerAngles = new Vector3(Mathf.Sin(Time.time * ballSpeed) * ballDistance, 0, 0); ;
            }
            
        }
    }
}
