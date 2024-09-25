using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    GameUI UI;

    void Start()
    {
        UI = FindObjectOfType<GameUI>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            UI.IncrementPoints();
            Destroy(gameObject);
        }
    }
}
