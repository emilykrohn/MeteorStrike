using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // https://gamedevbeginner.com/input-in-unity-made-easy-complete-guide-to-the-new-system/ (How to use New Unity Input System)
    // https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/Actions.html#polling-actions
    // https://discussions.unity.com/t/lookat-2d-equivalent/88118
    // CPSC386 Example02 InputExample.cs (New Unity Input System)
    // CPSC386 Example04 Player.cs (MovePosition)
    [SerializeField]
    PlayerInput playerInput;
    InputAction moveAction;
    [SerializeField]
    float moveSpeed = 10.0f;
    Vector2 moveDirection = Vector2.zero;
    Rigidbody2D rb;
    void Start()
    {
        // Get player rigidbody 2d to use physics movement
        rb = GetComponent<Rigidbody2D>();

        // Find movement action in player input component, this uses WASD
        moveAction = playerInput.actions.FindAction("Movement");

        // Enable movement action
        moveAction.Enable();
    }

    void Update()
    {
        if (playerInput.actions["Movement"].IsPressed())
        {
            // If one of the WASD keys is being pressed get the direction
            moveDirection = moveAction.ReadValue<Vector2>();
        } else {
            // else set the direction to zero so the player stops moving
            moveDirection = Vector2.zero;
        }

        /* The direction that the player is facing towards is the mouse
           Subtracting the mouse position from the player position gets the
           vector from the player pointing to the mouse and normalizing it just
           focuses on the direction and the magnitude is equal to 1 */
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();

        /* Since we have the vector pointing at the mouse, this give us
           (x, y) where x is the horizontal part of the triangle and y is the 
           vertical part of the triangle. We can use tan to find the degrees
           the player need to rotate to face the mouse */
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation - 90);
        
    }

    void FixedUpdate()
    {
        // Use physics to move the player
        rb.MovePosition(rb.position + (moveDirection * moveSpeed * Time.deltaTime));
    }
}
