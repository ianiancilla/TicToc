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
    float currentVelocity;

    // cached variables
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        // initialise stuff
        rigidBody = GetComponent<Rigidbody>();

        accelerationPerSec = moveSpeed / accelerationTime;
    }

    /// <summary>
    /// Moves character along X-axis.
    /// </summary>
    /// <param name="direction"></param>
    public void MoveHorizontally(float direction)
    {
        if (direction > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        float newXPos = transform.position.x + (direction * Time.deltaTime * moveSpeed);


        rigidBody.MovePosition(new Vector3(newXPos,
                                            transform.position.y,
                                            transform.position.z));
    }
}
