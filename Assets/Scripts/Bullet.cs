using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html (velocity)
// CPSC 396 Example03 Player.cs (OnTriggerEnter2D)
// https://docs.unity3d.com/ScriptReference/Component.CompareTag.html (CompareTag)
// https://www.youtube.com/watch?app=desktop&v=7NMsVub5NZM (Limits the bullet rigidbody velocity maginutude from being more than the maximum move speed)
// https://discussions.unity.com/t/volume-of-audio-clip-is-too-loud/135791 (Audio Clip)
public class Bullet : MonoBehaviour
{
    // Spawner script changes direction when bullet is spawned
    public Vector2 direction = Vector2.zero;
    [SerializeField]
    float moveSpeed = 3f;
    [SerializeField]
    AudioClip shootSound;
    [SerializeField]
    PlayerSaveData playerSaveData;
    Rigidbody2D rb;
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
        if (other.CompareTag("Meteor") || other.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}
