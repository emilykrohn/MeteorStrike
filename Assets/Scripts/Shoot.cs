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
    PlayerInput playerInput;
    [SerializeField]
    Bullet bulletPrefab;
    [SerializeField]
    float shootCooldown = 1f;
    float shootTimer = 0f;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer > shootCooldown)
        {
            if (playerInput.actions["Shooting"].IsPressed())
            {
                Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

                Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                direction.Normalize();

                bullet.direction = direction;
            }
            shootTimer = 0;
        }
    }
}
