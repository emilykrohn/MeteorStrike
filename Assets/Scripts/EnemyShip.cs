using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] int enemyHealth = 3;
    [SerializeField] float cooldown = 1f;
    [SerializeField] AudioClip hitSound;
    [SerializeField] PlayerSaveData playerSaveData;
    bool isPlayerTriggered = false;
    float timer = 0;
    GameUI UI;

    void Start()
    {
        // Find game object of type GameUI in the scene
        UI = FindObjectOfType<GameUI>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (isPlayerTriggered && timer > cooldown)
        {
            DamagePlayer();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerTriggered = true;
        }

        if (other.CompareTag("Bullet"))
        {
            enemyHealth -= 1;
            if (enemyHealth == 0)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position, playerSaveData.sfxVolume / 100);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerTriggered = false;
        }
    }

    void DamagePlayer()
    {
        // Call function in the game UI to decrease player lives and update UI
        UI.DecreaseHealth(10);
        // Play hit sound
        AudioSource.PlayClipAtPoint(hitSound, transform.position, playerSaveData.sfxVolume / 100);
        timer = 0;
    }
}
