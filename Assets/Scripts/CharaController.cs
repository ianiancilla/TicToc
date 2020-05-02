using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    // configuration variables
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] [Tooltip("Rate at which character gets to max speed.")]
                     [Range(0f, 1f)] float acceleration = 0.5f;
    [SerializeField] [Tooltip("Rate at which character stops moving once input ends.")]
    [Range(0f, 1f)] float deceleration = 0.5f;

    // cached variables
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        // initialise cache
        rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Moves character along X-axis.
    /// </summary>
    /// <param name="direction"></param>
    public void MoveHorizontally(float direction)
    {
        float newXPos = transform.position.x + (direction * Time.deltaTime * moveSpeed);
        rigidbody.MovePosition(new Vector3(newXPos,
                                            transform.position.y,
                                            transform.position.z));
    }
}
