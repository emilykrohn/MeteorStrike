using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CPSC 386 Example03 Player.cs (OnTriggerEnter2D)
// https://docs.unity3d.com/ScriptReference/Object.FindObjectOfType.html

public class Meteor : MonoBehaviour
{
    GameUI UI;
    void Start()
    {
        UI = FindObjectOfType<GameUI>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UI.DecrementLives();
            Destroy(gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
