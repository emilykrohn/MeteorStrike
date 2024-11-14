using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] int enemyHealth = 3;
    [SerializeField] float damageCooldown = 1f;
    [SerializeField] float shootCooldown = 1f;
    [SerializeField] AudioClip hitSound;
    [SerializeField] PlayerSaveData playerSaveData;
    [SerializeField] EnemyShipBullet enemyShipBullet;
    bool isPlayerTriggered = false;
    Vector3 direction;
    float damageTimer = 0;
    float shootTimer = 0;
    GameUI UI;
    GameObject player;

    void Start()
    {
        // Find game object of type GameUI in the scene
        UI = FindObjectOfType<GameUI>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        damageTimer += Time.deltaTime;
        if (isPlayerTriggered && damageTimer > damageCooldown)
        {
            DamagePlayer();
        }
        
        /* Subtracting target position (player) from the current position (enemyship)
           gives a vector pointing from the meteor to the player */
        direction = player.transform.position - transform.position;

        /* To keep the direction of the vector but make the maginute 1
           Normalize is used. This way the speed stays the same and doesn't
           depend on the distance the meteor is away from the player */
        direction.Normalize();

        shootTimer += Time.deltaTime;
        if (shootTimer > shootCooldown)
        {
            EnemyShipShoot();
            shootTimer = 0;
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
        damageTimer = 0;
    }

    void EnemyShipShoot()
    {
        /* If the amount of time on the timer is greater than the amount of time the cooldown has
            then spawn a bullet */
        EnemyShipBullet bullet = Instantiate(enemyShipBullet, transform.position, transform.rotation);

        // Have the bullet's direction facing the same direction as the EnemyShip
        bullet.direction = direction;
                
        // Reset the timer
        shootTimer = 0;
    }
}
