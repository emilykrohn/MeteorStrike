using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/Object.FindObjectOfType.html
// CPSC 396 Example03 Player.cs (OnTriggerEnter2D)
public class Star : MonoBehaviour
{
    GameUI UI;

    void Start()
    {
        // Find object with GameUI script in the scene
        UI = FindObjectOfType<GameUI>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            // Use function from the GameUI script
            UI.IncrementPoints();
            Destroy(gameObject);
        }
    }
}
