using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html
public class Bullet : MonoBehaviour
{
    public Vector2 direction = Vector2.zero;
    [SerializeField]
    float moveSpeed = 3f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb.velocity = direction * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Meteor") || other.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}
