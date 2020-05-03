using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;

    // cache
    CharaController charaController;

    private void Start()
    {
        charaController = GetComponent<CharaController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        charaController.MoveHorizontally(horizontalInput);
        //charaController.MoveVertically(verticalInput);
        //charaController.MoveZY(horizontalInput, verticalInput);
    }
}
