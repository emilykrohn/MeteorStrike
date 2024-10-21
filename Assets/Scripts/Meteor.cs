using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// CPSC 386 Example03 Player.cs (OnTriggerEnter2D)
// CPSC 386 Example04 Enemy.cs (FindObjectOfType)
// https://docs.unity3d.com/ScriptReference/Component.CompareTag.html (CompareTag)
// https://docs.unity3d.com/ScriptReference/Object.FindObjectOfType.html
// CPSC 386 Example02 Timer1.cs
public class Meteor : MonoBehaviour
{
    [SerializeField] AudioClip hitSound;
    [SerializeField] PlayerSaveData playerSaveData;
    [SerializeField] int meteorPoints = 5;

    [SerializeField] float delay = 5f;
    float timer = 0;
    
    GameUI UI;
    void Start()
    {
        // Find game object of type GameUI in the scene
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Call function in the game UI to decrease player lives and update UI
            UI.DecreaseHealth(10);
            // Play hit sound
            AudioSource.PlayClipAtPoint(hitSound, transform.position, playerSaveData.sfxVolume / 100);
            Destroy(gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            UI.IncreasePoints(meteorPoints);
            // Play hit sound
            AudioSource.PlayClipAtPoint(hitSound, transform.position, playerSaveData.sfxVolume / 100);
            // Destroy this instance of a meteor
            Destroy(gameObject);
        }
    }
}
