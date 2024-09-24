using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    float meteorTimer = 0;
    float starTimer = 0;
    [SerializeField]
    GameObject meteor;
    [SerializeField]
    float meteorCoolDown = 1f;
    [SerializeField]
    float meteorSpawnRadius = 8f;
    [SerializeField]
    float meteorInnerRadius = 4f;
    [SerializeField]
    GameObject star;
    [SerializeField]
    float starCoolDown = 0.5f;
    [SerializeField]
    float starSpawnRadius = 8f;
    [SerializeField]
    float starInnerRadius = 4f;

    private void Spawn(GameObject obj, float spawnRadius, float innerRadius)
    {
        bool validLocation = false;
        while(!validLocation)
        {
            Vector2 currentPos = transform.position;
            float xPos = Random.Range(-spawnRadius + currentPos.x, spawnRadius + currentPos.x);
            float yPos = Random.Range(-spawnRadius + currentPos.y, spawnRadius + currentPos.y);
            if (xPos > innerRadius + currentPos.x || xPos < -innerRadius + currentPos.x ||
                yPos > innerRadius + currentPos.y || yPos < -innerRadius + currentPos.y)
            {
                validLocation = true;
                GameObject currentObj = Instantiate(obj);
                currentObj.transform.position = new Vector3(xPos, yPos);
            }
        }
    }

    private void Update()
    {
        meteorTimer += Time.deltaTime;
        starTimer += Time.deltaTime;
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
    }
}
