using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/Object.FindObjectOfType.html
// CPSC 396 Example03 Player.cs (OnTriggerEnter2D)
// CPSC 386 Example04 Enemy.cs (FindObjectOfType)
// https://docs.unity3d.com/ScriptReference/Component.CompareTag.html (CompareTag)
// CPSC 386 Example02 Timer1.cs
// https://discussions.unity.com/t/volume-of-audio-clip-is-too-loud/135791 (Audio Clip)
public class Star : MonoBehaviour
{
    [SerializeField] AudioClip collectSound;
    [SerializeField] PlayerSaveData playerSaveData;
    [SerializeField] int starPoints = 3;
    
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
            // Play sound clip at current volume level
            AudioSource.PlayClipAtPoint(collectSound, transform.position, playerSaveData.sfxVolume / 100);
            // Use function from the GameUI script to increase player points
            UI.IncreasePoints(starPoints);
            Destroy(gameObject);
        }
    }
}