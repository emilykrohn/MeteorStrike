using System;
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

    // https://docs.unity3d.com/ScriptReference/Mathf.Lerp.html (Lerp)
    // https://docs.unity3d.com/ScriptReference/Mathf.Min.html (Min)

    // https://www.youtube.com/watch?app=desktop&v=7NMsVub5NZM (Limits the player rigidbody velocity maginutude from being more than the maximum move speed)

    [SerializeField] PlayerInput playerInput;
    InputAction moveAction;
    PlayerData playerData;

    // Lerp
    float moveSpeedMinimum = 0f;
    public float moveSpeedMaximum;
    float time = 0f;

    Vector2 moveDirection = Vector2.zero;
    Vector2 previousMoveDirection = Vector2.zero;
    Rigidbody2D rb;

    void Start()
    {
        playerData = GetComponent<PlayerData>();
        // Get player rigidbody 2d to use physics movement
        rb = GetComponent<Rigidbody2D>();        

        // Find movement action in player input component, this uses WASD
        moveAction = playerInput.actions.FindAction("Movement");

        // Enable movement action
        moveAction.Enable();
    }

    void Update()
    {
        moveSpeedMaximum = playerData.current_speed;
        if (playerInput.actions["Movement"].IsPressed())
        {
            // If one of the WASD keys is being pressed get the direction
            moveDirection = moveAction.ReadValue<Vector2>();

            // Used when player is moving, if moveDirection is zero then can keep going in direction but slow down 
            previousMoveDirection = moveDirection;
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
        /* At 0, moveSpeed is moveSpeedMinimum and at 1, moveSpeed is moveSpeedMaximum
           As time increases or decreases by decimal values, it moves smoothly between the two moveSpeed min and max valeus */
        float moveSpeed = Mathf.Lerp(moveSpeedMinimum, moveSpeedMaximum, time);

        // If the player is moving
        if (moveDirection != Vector2.zero)
        {
            time += Time.deltaTime;
            // Make sure that time doesn't become a higher number than 1
            time = Mathf.Min(time, 1);
        } else {
            time -= 0.05f - Time.deltaTime;
            // Make sure that time doesn't become a lower number than 0
            time = Mathf.Max(time, 0);
        }

        // Use physics to move the player
        if (moveDirection == Vector2.zero)
        {
            /* If use moveDirection in this case then it would immediately become 0 because it would multiply by 0
               it should equal zero when the move speed is zero and not the direction */
            rb.MovePosition(rb.position + (previousMoveDirection * moveSpeed * Time.deltaTime));
        } else {
            rb.MovePosition(rb.position + (moveDirection * moveSpeed * Time.deltaTime));
        }

        // rb.velocity.maginitude is the speed of the player
        if (rb.velocity.magnitude > moveSpeedMaximum)
        {
            // Makes sure the velicity doesn't go past the move speed maximum
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, moveSpeedMaximum);
        }
    }
}
