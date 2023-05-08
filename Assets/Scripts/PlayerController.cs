using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        WAIT,
        NORMAL,
        DEAD,
        VICTORY,
    }
    private const float gravity = -9.81f;
    CameraController cameraScript;
    [HideInInspector]
    public State currentState;

    [HideInInspector]
    public Rigidbody rigidBody;

    ConstantForce constantForce;

    [HideInInspector]
    public Vector3 floorNormal;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private float groundCheckRadius;

    public float maxSpeed;

    [SerializeField]
    private float moveForce;
    [SerializeField]
    private float victoryForce;
    private Collider coll;


    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider>();

        cameraScript = FindObjectOfType<CameraController>();

        rigidBody = GetComponent<Rigidbody>();  // Get rigidbody component

        constantForce = GetComponent<ConstantForce>();

        currentState = State.WAIT;            // Set current state to WAIT
    }
    private void Update()
    {
        //gyroInput = Input.gyro.attitude;
        
    }
    void FixedUpdate()
    {
        switch (currentState)
        {
            case State.VICTORY:
                ApplyVictoryForce();
                break;
        }
    }

    /// <summary>
    /// Applies force to player object
    /// </summary>
    /// <param name="verticalTilt">Scales force applied in the forward direction (Ranges between 1 and -1)</param>
    /// <param name="horizontalTilt">Scales force applied in the horizontal direction (Ranges between 1 and -1)</param>
    /// <param name="right">The horizontal direction</param>
    public void Move(float verticalTilt, float horizontalTilt, Vector3 right)
    {
        // Only apply movement when the player is grounded
/*        if (OnGround())
        {*/
            CalculateFloorNormal();

            // No input from player
            if (horizontalTilt == 0.0f && verticalTilt == 0.0f && rigidBody.velocity.magnitude > 0.0f)
            {
                rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, Vector3.zero, moveForce * 0.1f * Time.deltaTime); // Slow down
            }
            else
            {
                // Apply moveForce scaled by verticalTilt in the forward direction (Half the move force when moving backwards)
                Vector3 forwardForce = (verticalTilt > 0.0f ? 1.0f : 0.5f) * moveForce * verticalTilt * Vector3.forward;
                // Apply moveForce scaled by horizontalTilt in the right direction
                Vector3 rightForce = moveForce * horizontalTilt * right;

                Vector3 forceVector = forwardForce + rightForce;

                rigidBody.AddForce(forceVector);
            }
        /*}*/
    }

    /// <summary>
    /// Checks if the player is grounded (Above an object within whatIsGround layer mask)
    /// </summary>
    /// <returns>
    /// True if CheckSphere overlaps with collider within the whatIsGround layer mask, else return False
    /// </returns>
    public bool OnGround()
    {
        return Physics.CheckSphere(transform.position - (Vector3.up * 0.5f), groundCheckRadius, whatIsGround);
    }

    /// <summary>
    /// Applies an upward force to the player to lift them up to the next stage
    /// </summary>
    private void ApplyVictoryForce()
    {
        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0.0f, rigidBody.velocity.z);

        // Move in the opposite direction of velocity minus the y component
        if (flatVel.magnitude > 0.1f)
            rigidBody.AddForce(-flatVel);


        // Increment force in the up direction
        rigidBody.AddForce(victoryForce++ * Vector3.up);
    }

    /// <summary>
    /// Sets floor normal by casting ray below player
    /// </summary>
    private void CalculateFloorNormal()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, whatIsGround))
        {
            floorNormal = hit.normal;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Right":
                constantForce.force = new Vector3(-gravity, -gravity, 0);
                cameraScript.currentGrav = CameraController.gravityDirection.Right;
                break;
            case "Left":
                constantForce.force = new Vector3(gravity, -gravity, 0);
                cameraScript.currentGrav = CameraController.gravityDirection.Left;
                break;
            case "Up":
                constantForce.force = new Vector3(0, -gravity * 2, 0);
                cameraScript.currentGrav = CameraController.gravityDirection.Up;
                break;
            case "Down":
                constantForce.force = Vector3.zero;
                cameraScript.currentGrav = CameraController.gravityDirection.Down;
                break;
            case "Forward":
                constantForce.force = new Vector3(0, -gravity, -gravity);
                cameraScript.currentGrav = CameraController.gravityDirection.Forward;
                break;
            case "Back":
                constantForce.force = new Vector3(0, -gravity, gravity);
                cameraScript.currentGrav = CameraController.gravityDirection.Back;
                break;
            case "KillPlane":
                currentState = State.DEAD;
                StartCoroutine(DeathWait());
                break;
            case "Win":
                currentState = State.VICTORY;
                coll.isTrigger = true;
                StartCoroutine(Victory());
                break;
        }
    }
    private IEnumerator DeathWait()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private IEnumerator Victory()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}