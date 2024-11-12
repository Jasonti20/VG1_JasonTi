using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : MonoBehaviour
    {
        // Rigidbody reference for physics
        public Rigidbody2D _rb;

        // Target for the projectile to home in on
        public Transform target;

        // Speed and acceleration variables
        public float acceleration = 1f;
        public float maxSpeed = 2f;

        void Start()
        {
            // Get the Rigidbody2D component
            _rb = GetComponent<Rigidbody2D>();

            // Find the nearest asteroid target at the start
            ChooseNearestTarget();
        }

        void Update()
        {
            float acceleration = GameController.instance.missileSpeed / 2f;
            float maxSpeed = GameController.instance.missileSpeed;
            // If a target has been chosen, adjust rotation towards it
            if (target != null)
            {
                // Calculate the direction to the target
                Vector2 directionToTarget = (Vector2)target.position - _rb.position;

                // Calculate the angle and rotate towards the target
                float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                _rb.MoveRotation(angle);

                // Apply acceleration towards the right
                _rb.AddForce(transform.right * acceleration);

                // Clamp the velocity to ensure it doesn't exceed maxSpeed
                _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxSpeed);
            }
        }

        // Function to find the nearest asteroid target
        void ChooseNearestTarget()
        {
            // Set a high default value to ensure any asteroid becomes the first valid target
            float closestDistance = Mathf.Infinity;

            // Find all asteroids in the scene
            Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

            // Loop through all asteroids and find the closest one
            foreach (Asteroid asteroid in asteroids)
            {
                // Only consider asteroids to the right of the projectile
                if (asteroid.transform.position.x > transform.position.x)
                {
                    // Calculate the distance between the projectile and the asteroid
                    Vector2 directionToTarget = asteroid.transform.position - transform.position;
                    float distanceToTarget = directionToTarget.sqrMagnitude;  // Use square magnitude for efficiency

                    // If this asteroid is the closest, select it as the target
                    if (distanceToTarget < closestDistance)
                    {
                        closestDistance = distanceToTarget;
                        target = asteroid.transform;
                    }
                }
            }
        }

        // Function to handle collision with asteroids
        void OnCollisionEnter2D(Collision2D other)
        {
            // Check if the object we hit is an asteroid
            if (other.gameObject.GetComponent<Asteroid>())
            {
                // Destroy the asteroid and the projectile
                Destroy(other.gameObject);  // Destroy asteroid
                Destroy(gameObject);  // Destroy the projectile

                // Instantiate explosion effect at the collision position
                GameObject explosion = Instantiate(GameController.instance.explosionPrefab, transform.position, Quaternion.identity);

                // Destroy the explosion effect after a short delay
                Destroy(explosion, 0.25f);

                GameController.instance.EarnPoints(10);
            }
        }
    }
}
