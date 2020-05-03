using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    // configuration variables
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    
    // cached variables
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        // initialise stuff
        rigidBody = GetComponent<Rigidbody>();

    }

    /// <summary>
    /// Moves character horizontally along Z-axis, with max move speed dictated by moveSpeed.
    /// </summary>
    /// <param name="direction"></param>
    public void MoveHorizontally(float direction)
    {
        float deltaMove = moveSpeed * direction * Time.deltaTime;

        transform.position = new Vector3(transform.position.x,
                                         transform.position.y,
                                         transform.position.z + deltaMove);
    }

    /// <summary>
    /// Rotates character on Y-axis to face direction it moves in, when moving on Z-axis.
    /// </summary>
    /// <param name="horDirection"></param>
    public void FaceHorDirection(float horDirection)
    {
        // face correct direction
        if (horDirection > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horDirection < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

}
