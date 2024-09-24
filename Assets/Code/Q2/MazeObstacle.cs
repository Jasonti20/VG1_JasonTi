using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Q2 {
    public class MazeObstacle : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D other) {
            // Reload scene only when colliding with player
            if(other.gameObject.GetComponent<RotatingShipController>()) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
