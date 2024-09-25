using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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
