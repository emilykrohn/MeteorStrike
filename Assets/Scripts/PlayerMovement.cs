using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/Actions.html#polling-actions
    // https://discussions.unity.com/t/lookat-2d-equivalent/88118
    // CS386 Example02 InputExample.cs
    [SerializeField]
    PlayerInput playerInput;
    InputAction moveAction;
    [SerializeField]
    float moveSpeed = 10.0f;
    Vector2 position;
    Vector2 moveDirection = Vector2.zero;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        position = transform.position;
        moveAction = playerInput.actions.FindAction("Movement");
        moveAction.Enable();
    }

    void Update()
    {
        if (playerInput.actions["Movement"].IsPressed())
        {
            moveDirection = moveAction.ReadValue<Vector2>();
        } else {
            moveDirection = Vector2.zero;
        }

        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation - 90);
        
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (moveDirection * moveSpeed * Time.deltaTime));
    }
}
