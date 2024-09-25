using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://stackoverflow.com/questions/30310847/gameobject-findobjectoftype-vs-getcomponent
// https://docs.unity3d.com/ScriptReference/Object.Destroy.html
// https://docs.unity3d.com/ScriptReference/Vector3.Normalize.html
// https://docs.unity3d.com/ScriptReference/Object.Destroy.html
public class MeteorMovement : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    float moveSpeed = 5f;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        direction = player.transform.position - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
