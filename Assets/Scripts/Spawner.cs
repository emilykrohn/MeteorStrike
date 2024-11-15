using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CPSC 386 Example02 Timer1.cs
// CPSC 386 Example04 EnemyManager.cs (Spawn Radius)
// https://docs.unity3d.com/2022.3/Documentation/ScriptReference/HeaderAttribute.html (Header Attribute)
public class Spawner : MonoBehaviour
{
    PlayerData player;
    float meteorTimer = 0;
    float starTimer = 0;
    float enemyShipTimer = 0;
    float chargeShipTimer = 0;

    [Header("Meteor")]
    [SerializeField] GameObject meteor;
    [SerializeField] float meteorCoolDown = 1f;
    [SerializeField] float meteorSpawnRadius = 8f;
    [SerializeField] float meteorInnerRadius = 4f;

    [Header("EnemyShip")]
    [SerializeField] GameObject enemyShip;
    [SerializeField] float enemyShipCoolDown = 1f;
    [SerializeField] float enemyShipSpawnRadius = 8f;
    [SerializeField] float enemyShipInnerRadius = 4f;

    [Header("ChargeShip")]
    [SerializeField] GameObject chargeShip;
    [SerializeField] float chargeShipCoolDown = 1f;
    [SerializeField] float chargeShipSpawnRadius = 8f;
    [SerializeField] float chargeShipInnerRadius = 4f;

    [Header("Star")]
    [SerializeField] GameObject star;
    [SerializeField] float starCoolDown = 0.5f;
    [SerializeField] float starSpawnRadius = 8f;
    [SerializeField] float starInnerRadius = 4f;

    void Start()
    {
        player = GetComponent<PlayerData>();
    }

    // Spawns in specified game object with inputed spawn radius and inner radius
    private void Spawn(GameObject obj, float spawnRadius, float innerRadius)
    {
        /* Once it finds a position that is outside of th inner radius and inside the spawn radius
           validLocation become true and the loop stops and the game object is spawned */
        bool validLocation = false;
        while(!validLocation)
        {
            // Uses the current player position so the game object spawns relative to the player
            Vector2 currentPos = transform.position;

            // Finds random x and y position inside of the spawn radius
            float xPos = Random.Range(-spawnRadius + currentPos.x, spawnRadius + currentPos.x);
            float yPos = Random.Range(-spawnRadius + currentPos.y, spawnRadius + currentPos.y);

            // Checks if both x and y positions are outside of the inner radius
            if (xPos > innerRadius + currentPos.x || xPos < -innerRadius + currentPos.x ||
                yPos > innerRadius + currentPos.y || yPos < -innerRadius + currentPos.y)
            {
                validLocation = true;
                GameObject currentObj = Instantiate(obj);
                currentObj.transform.position = new Vector3(xPos, yPos);
            }
        }
    }

    // When the meteor or star timer has more time than the cooldown, then it spawns the game object
    private void Update()
    {
        meteorTimer += Time.deltaTime;
        starTimer += Time.deltaTime;
        enemyShipTimer += Time.deltaTime;
        chargeShipTimer += Time.deltaTime;
        
        if (meteorTimer > meteorCoolDown)
        {
            Spawn(meteor, meteorSpawnRadius, meteorInnerRadius);
            meteorTimer = 0;
        }
        if (starTimer > starCoolDown)
        {
            Spawn(star, starSpawnRadius, starInnerRadius);
            starTimer = 0;
        }
        if (enemyShipTimer > enemyShipCoolDown && player.current_level >= 2)
        {
            Spawn(enemyShip, enemyShipSpawnRadius, enemyShipInnerRadius);
            enemyShipTimer = 0;
        }
        if (chargeShipTimer > chargeShipCoolDown && player.current_level >= 4)
        {
            Spawn(chargeShip, chargeShipSpawnRadius, chargeShipInnerRadius);
            chargeShipTimer = 0;
        }
    }
}
