using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://stackoverflow.com/questions/30310847/gameobject-findobjectoftype-vs-getcomponent (FindGameObjectWithTag)
// https://docs.unity3d.com/ScriptReference/Object.Destroy.html (Destroy)
// https://docs.unity3d.com/ScriptReference/Vector3.Normalize.html (Normalize)
// CPSC 386 Example04 Enemy.cs (Updating current position)
public class MeteorMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Vector3 direction;
    
    GameObject player;

    /* Getting the direction the meteor is going to move from in start
       so the meteor goes towards the player initially but don't keep following the player */
    void Start()
    {
        // Find player Game Object
        player = GameObject.FindGameObjectWithTag("Player");
        
        /* Subtracting target position (player) from the current position (meteor)
           gives a vector pointing from the meteor to the player */
        direction = player.transform.position - transform.position;

        /* To keep the direction of the vector but make the maginute 1
           Normalize is used. This way the speed stays the same and doesn't
           depend on the distance the meteor is away from the player */
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        // Use direction, move speed, and Time.deltaTime to update the current position
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
