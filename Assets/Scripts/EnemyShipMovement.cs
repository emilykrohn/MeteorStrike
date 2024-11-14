using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    GameObject player;

    void Update()
    {
        // Find player Game Object
        player = GameObject.FindGameObjectWithTag("Player");
        
        /* Subtracting target position (player) from the current position (meteor)
           gives a vector pointing from the meteor to the player */
        Vector3 direction = player.transform.position - transform.position;

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
