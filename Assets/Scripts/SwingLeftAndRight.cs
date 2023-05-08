using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingLeftAndRight : MonoBehaviour
{
    [SerializeField] float ballSpeed;
    [SerializeField] float ballDistance;
    private void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time * ballSpeed) * ballDistance);
    }
}
