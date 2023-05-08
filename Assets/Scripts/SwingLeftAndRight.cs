using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingLeftAndRight : MonoBehaviour
{
    [SerializeField] float ballSpeed;
    [SerializeField] float ballDistance;
    [SerializeField] bool leftAndRight;
    [SerializeField] bool upAndDown;
    private void FixedUpdate()
    {
       if (leftAndRight) transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time * ballSpeed) * ballDistance);
       if (upAndDown) transform.eulerAngles = new Vector3(Mathf.Sin(Time.time * ballSpeed) * ballDistance, 0, 0);
    }
}
