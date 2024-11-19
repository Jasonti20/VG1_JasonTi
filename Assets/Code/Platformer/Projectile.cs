using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Projectile : MonoBehaviour
    {
        // Outlets
        Rigidbody2D _rigidbody2D;

        // Methods
        void Start()
        {
            // Get the Rigidbody2D component attached to the projectile
            _rigidbody2D = GetComponent<Rigidbody2D>();

            // Set the velocity of the projectile to move in the direction it's facing
            _rigidbody2D.velocity = transform.right * 10f;  // Adjust speed as necessary
        }



        // Handle collision
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Target>())
            {
                SoundManager.instance.PlaySoundHit();
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                SoundManager.instance.PlaySoundMiss();
            }

            Destroy(gameObject);
        }
    }
}
