using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// https://docs.unity3d.com/Packages/com.unity.inputsystem@1.7/manual/Actions.html#polling-actions
// https://discussions.unity.com/t/lookat-2d-equivalent/88118
// CPSC 386 Example02 Timer1.cs
public class Shoot : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    PlayerInput playerInput;
    PlayerData playerData;
    
    float shootCooldown;
    float shootTimer = 0f;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerData = GetComponent<PlayerData>();
    }
    void Update()
    {
        shootCooldown = playerData.current_fire_rate;
        /* Add the time between frames to the timer so the timer will be the amount of time that has passed since the scene started or
           since the last reset when a bullet is shot */
        shootTimer += Time.deltaTime;

        if (shootTimer > shootCooldown)
        {
            if (playerInput.actions["Shooting"].IsPressed())
            {
                /* If the amount of time on the timer is greater than the amount of time the cooldown has
                   then spawn a bullet */
                Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

                // Get the Vector pointing to the mouse from the player position
                Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                direction.Normalize();

                // Have the bullet's direction facing the same direction as the player
                bullet.direction = direction;
                
                // Reset the timer
                shootTimer = 0;
            }
            
        }
    }
}
