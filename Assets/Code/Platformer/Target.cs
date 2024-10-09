using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer {
    public class Target : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.GetComponent<Projectile>()) {
                Destroy(gameObject);
            }
        }
    }
}
