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
    }
}
