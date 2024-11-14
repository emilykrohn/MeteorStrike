using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipBullet : MonoBehaviour
{
    public Vector2 direction = Vector2.zero;
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 3f;

    [SerializeField] AudioClip shootSound;

    [SerializeField] PlayerSaveData playerSaveData;
    void Start()
    {
        // Get the Rigidbody2D of this current instance of the bullet to be able to use physics movement
        rb = GetComponent<Rigidbody2D>();
        AudioSource.PlayClipAtPoint(shootSound, transform.position, playerSaveData.sfxVolume / 100);
    }
    void FixedUpdate()
    {
        /* The rate of change of the bullet is equal to the normalized direction from the 
        player position and the mouse position multiplied by the move speed */
        rb.velocity = direction.normalized * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        // When the bullet hits a meteor or border, this current instance of the bullet gets destroyed
        if (other.CompareTag("Player") || other.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }

}
