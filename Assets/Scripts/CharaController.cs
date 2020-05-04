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
    ///  Moves character along Z-axis and Y-axis, with max move speed dictated by moveSpeed.
    /// </summary>
    /// <param name="directionZ">-1 to 1, 0 means no movement</param>
    /// <param name="directionY">-1 to 1, 0 means no movement</param>
    public void Move(float directionZ, float directionY)
    {
        float deltaZ = moveSpeed * directionZ * Time.deltaTime;
        float deltaY = moveSpeed * directionY * Time.deltaTime;


        transform.position = new Vector3(transform.position.x,
                                         transform.position.y + deltaY,
                                         transform.position.z + deltaZ);
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
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (horDirection < 0)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
    }

    /// <summary>
    /// Moves character to given position.
    /// </summary>
    /// <param name="targetPosition"></param>
    public void Teleport(Vector3 targetPos)
    {
        transform.position = targetPos;
    }

    public void MoveToPosCoroutine(Vector3 targetPos)
    {
        StartCoroutine(MoveToPos(targetPos));
    }

    IEnumerator MoveToPos(Vector3 targetPos)
    {
        float deltaMove = moveSpeed * Time.deltaTime;

        yield return transform.position = Vector3.MoveTowards(transform.position,
                                                              targetPos,
                                                              deltaMove);
    }

}
