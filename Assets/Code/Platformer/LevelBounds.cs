using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer {
    public class LevelBounds : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.GetComponent<PlayerController>()) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
