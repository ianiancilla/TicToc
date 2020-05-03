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
                     float decelerationTime = 1f;

    // private variables movement
    float accelerationPerSec;
    float decelerationPerSec;
    float currentSpeed = 0;
    int currentHorDir = 1; // these two indicate current facing direction. 1 for positive, -1 for negative.
    int currentVerDir = 1; 

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
    /// Moves character along Z-axis ONLY, turning to face direction of movement.
    /// Cannot be used together with MoveVertically. To use together, use MoveZY.
    /// Starting/stopping speed indicated by Characontroller's accelerationTime/deceleration/time variables.
    /// </summary>
    /// <param name="direction">Only sign is meaningful. Negative values move left, positive move right.</param>
    public void MoveHorizontally(float direction)
    {
        var targetPos = new Vector3(transform.position.x,
                                    transform.position.y,
                                    CalculateTargetZ(direction));

        var deltaMove = CalculateDeltaMove(IsAccelerating(direction));
        transform.position = Vector3.MoveTowards(transform.position,
                                                 targetPos,
                                                 deltaMove);
    }

    /// <summary>
    /// Moves character along Y-axis ONLY.
    /// Cannot be used together with MoveHorizontally. To use together, use MoveZY.
    /// Starting/stopping speed indicated by Characontroller's accelerationTime/deceleration/time variables.
    /// </summary>
    /// <param name="direction">Only sign is meaningful. Negative values move dowm, positive move up.</param>
    public void MoveVertically(float direction)
    {
        var targetPos = new Vector3(transform.position.x,
                                    CalculateTargetY(direction),
                                    transform.position.z);

        var deltaMove = CalculateDeltaMove(IsAccelerating(direction));
        transform.position = Vector3.MoveTowards(transform.position,
                                                 targetPos,
                                                 deltaMove);
    }


    /// <summary>
    /// Moves character on the YZ plane according to given directional inputs.
    /// Starting/stopping speed indicated by Characontroller's accelerationTime/deceleration/time variables.
    /// </summary>
    /// <param name="horDirection">Only sign is meaningful.</param>
    /// <param name="verDirection">Only sign is meaningful.</param>
    public void MoveZY(float horDirection, float verDirection)
    {
        float targetY = transform.position.y;
        float targetZ = transform.position.z;

        if (verDirection != 0) { targetY = CalculateTargetY(verDirection); }
        if (horDirection != 0) { targetZ = CalculateTargetZ(horDirection); }

        var targetPos = new Vector3(transform.position.x,
                                    targetY,
                                    targetZ);

        var deltaMove = CalculateDeltaMove(IsAccelerating(horDirection) | IsAccelerating(verDirection));

        transform.position = Vector3.MoveTowards(transform.position,
                                                 targetPos,
                                                 deltaMove);                                               
    }

    private float CalculateDeltaMove(bool accelerating)
    {
        // define speed based on acceleration/deceleration
        if (accelerating)
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
        return deltaMove;
    }
    
    private bool IsAccelerating(float direction)
    {
        if (direction == 0) { return false; }
        else { return true; }
    }
    
    private void FaceHorDirection(float horDirection)
    {
        // face correct direction
        if (horDirection > 0)
        {
            currentHorDir = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horDirection < 0)
        {
            currentHorDir = -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    
    private void FaceVerDirection(float verDirection)
    {
        // face correct direction
        if (verDirection > 0)
        {
            currentVerDir = 1;
        }
        else if (verDirection < 0)
        {
            currentVerDir = -1;
        }
    }

    private float CalculateTargetZ(float direction)
    {
        FaceHorDirection(direction);
        float newZPos = transform.position.z + (moveSpeed * currentHorDir);
        return newZPos;
    }

    private float CalculateTargetY(float direction)
    {
        FaceVerDirection(direction);
        float newYPos = transform.position.y + (moveSpeed * currentVerDir);
        return newYPos;        
    }

}
