using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class MeteorSpawner : MonoBehaviour
{
    float timer = 0;
    [SerializeField]
    float coolDown = 0.5f;
    [SerializeField]
    float spawnRadius = 8f;
    [SerializeField]
    float innerRadius = 4f;
    [SerializeField]
    GameObject meteor;

    private void SpawnMeteor()
    {
        bool validLocation = false;
        while(!validLocation)
        {
            Vector2 currentPos = transform.position;
            GameObject currentMeteor = Instantiate(meteor);
            float xPos = Random.Range(-spawnRadius + currentPos.x, spawnRadius + currentPos.x);
            float yPos = Random.Range(-spawnRadius + currentPos.y, spawnRadius + currentPos.y);
            if (xPos > innerRadius || xPos < -innerRadius ||
                yPos > innerRadius || yPos < -innerRadius)
            {
                validLocation = true;
                currentMeteor.transform.position = new Vector3(xPos, yPos);
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > coolDown)
        {
            SpawnMeteor();
            timer = 0;
        }
    }
}
