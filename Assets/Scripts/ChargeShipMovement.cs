using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChargeShipMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float chargeCooldown = 1f;
    [SerializeField] float chargeLengthCooldown = 1f;
    [SerializeField] float chargeSpeed = 5f;
    Vector3 direction;
    bool increasedSpeed;
    float chargeTimer = 0;
    float chargeLengthTimer = 0;
    ChargeShip chargeShip;
    GameObject player;

    void Start()
    {
        chargeShip = GetComponent<ChargeShip>();
        // Find player Game Object
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        chargeTimer += Time.deltaTime;
        if (chargeTimer > chargeCooldown && !chargeShip.isPlayerTriggered)
        {
            if (!increasedSpeed)
            {
                moveSpeed += chargeSpeed;
                increasedSpeed = true;
            }

            chargeLengthTimer += Time.deltaTime;
            if (chargeLengthTimer > chargeLengthCooldown)
            {
                moveSpeed -= chargeSpeed;
                increasedSpeed = false;
                chargeLengthTimer = 0;
                chargeTimer = 0;
            }
        }
        
        /* Subtracting target position (player) from the current position (enemyship)
           gives a vector pointing from the meteor to the player */

        if (!increasedSpeed)
        {
            direction = player.transform.position - transform.position;
        }

        /* To keep the direction of the vector but make the maginute 1
           Normalize is used. This way the speed stays the same and doesn't
           depend on the distance the meteor is away from the player */
        direction.Normalize();

        // Update Enemy ship's position
        transform.position += direction * moveSpeed * Time.deltaTime;

        /* Since we have the vector pointing at the player, this give us
           (x, y) where x is the horizontal part of the triangle and y is the 
           vertical part of the triangle. We can use tan to find the degrees
           the player need to rotate to face the mouse */
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation - 90);
    }
}
