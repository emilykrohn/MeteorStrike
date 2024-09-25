using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
