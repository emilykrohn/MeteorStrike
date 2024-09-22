using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/Actions.html#polling-actions
    [SerializeField]
    PlayerInput playerInput;
    InputAction moveAction;
    [SerializeField]
    float moveSpeed = 10.0f;
    Vector2 position;

    void Start()
    {
        position = transform.position;
        moveAction = playerInput.actions.FindAction("Movement");
        moveAction.Enable();
    }

    void Update()
    {
        if (playerInput.actions["Movement"].IsPressed())
        {
            Vector2 moveDirection = moveAction.ReadValue<Vector2>();
            position += moveDirection * moveSpeed * Time.deltaTime;
            transform.position = position;
        }
    }
}
