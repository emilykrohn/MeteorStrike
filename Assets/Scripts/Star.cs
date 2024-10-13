using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/Object.FindObjectOfType.html
// CPSC 396 Example03 Player.cs (OnTriggerEnter2D)
// CPSC 386 Example04 Enemy.cs (FindObjectOfType)
// https://docs.unity3d.com/ScriptReference/Component.CompareTag.html (CompareTag)
// CPSC 386 Example02 Timer1.cs
public class Star : MonoBehaviour
{
    float timer = 0;
    float delay = 10;
    GameUI UI;

    void Start()
    {
        // Find object with GameUI script in the scene
        UI = FindObjectOfType<GameUI>();
    }
    void Update()
    {
        // After a delay, delete the game object
        timer += Time.deltaTime;
        if (timer > delay)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            // Use function from the GameUI script
            UI.IncreasePoints(1);
            Destroy(gameObject);
        }
    }
}
