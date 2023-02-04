using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float speedPlayer;

    private void Update()
    {
        MovePlayer();
        LimitY();
    }

    void MovePlayer()
    {
        float horizontalInput = SimpleInput.GetAxis("Horizontal");
        float verticalInput = SimpleInput.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontalInput, verticalInput, 0);

        characterController.Move(move * speedPlayer * Time.deltaTime);
    }

    void LimitY()
    {
        if (transform.position.y >= 5) transform.position = new Vector3(transform.position.x, 5, transform.position.z);
        if (transform.position.y <= -5) transform.position = new Vector3(transform.position.x, -5, transform.position.z);
    }
}
