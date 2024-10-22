using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    public class Ship : MonoBehaviour
    {
        // Outlet
        public GameObject projectilePrefab;

        // State Tracking
        public float firingDelay = 1f;

        // Methods

        // Function to fire a projectile
        void FireProjectile()
        {
            // Instantiate the projectile at the ship's position with no rotation
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }

        // Coroutine that manages the firing delay
        IEnumerator FiringTimer()
        {
            // Wait for the delay before firing again
            yield return new WaitForSeconds(firingDelay);

            // Fire the projectile
            FireProjectile();

            // Start the coroutine again to repeat firing
            StartCoroutine("FiringTimer");
        }

        // Start the firing timer when the game begins
        void Start()
        {
            // Start the firing loop
            StartCoroutine("FiringTimer");
        }
    }
}
