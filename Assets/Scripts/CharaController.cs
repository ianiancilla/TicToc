using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    // configuration variables
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] [Tooltip("Time it takes to reach max speed")]
                     float accelerationTime = 2.5f;
    [SerializeField] [Tooltip("Time it takes to stop completely.")]
                     float decelerationTime = 2.5f;

    // private variables
    float accelerationPerSec;
    float decelerationPerSec;
    float currentSpeed = 0;

    // cached variables
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        // initialise stuff
        rigidBody = GetComponent<Rigidbody>();

        accelerationPerSec = moveSpeed / accelerationTime;
        decelerationPerSec = moveSpeed / decelerationTime;
    }

    /// <summary>
    /// Moves character along X-axis, turning to face directino of movement.
    /// Starting/stopping speed indicated by accelerationTime/deceleration/time variables.
    /// </summary>
    /// <param name="direction"></param>
    public void MoveHorizontally(float direction)
    {
        // face correct direction
        if (direction > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        // define speed based on acceleration/deceleration
        if (direction != 0)
        {
            currentSpeed = Mathf.Clamp(currentSpeed + (accelerationPerSec * Time.deltaTime), 
                                       0,
                                       moveSpeed);
        }
        else
        {
            currentSpeed = Mathf.Clamp(currentSpeed - (decelerationPerSec * Time.deltaTime),
                                       0, 
                                       moveSpeed);
        }

        // move
        float deltaMove = (Time.deltaTime * currentSpeed);
        float newXPos;

        if (transform.rotation.y == 0)
        {
            newXPos = transform.position.x + deltaMove;
        }
        else
        {
            newXPos = transform.position.x - deltaMove;

        }

        rigidBody.MovePosition(new Vector3(newXPos,
                                           transform.position.y,
                                           transform.position.z));
    }
}
