using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CPSC 386 Example03 Player.cs (OnTriggerEnter2D)
// CPSC 386 Example04 Enemy.cs (FindObjectOfType)
// https://docs.unity3d.com/ScriptReference/Object.FindObjectOfType.html

public class Meteor : MonoBehaviour
{
    GameUI UI;
    void Start()
    {
        // Find game object of type GameUI in the scene
        UI = FindObjectOfType<GameUI>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Call function in the game UI to decrease player lives and update UI
            UI.DecrementLives();
            Destroy(gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            // Destroy this instance of a meteor
            Destroy(gameObject);
        }
    }
}
