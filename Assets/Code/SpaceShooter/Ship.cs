using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class Ship : MonoBehaviour
    {
        // Outlet
        public GameObject projectilePrefab;
        public Image imageHealthBar;
        public TMP_Text hullUpgradeText;
        public TMP_Text fireSpeedUpgradeText;

        // State Tracking
        public float firingDelay = 1f;
        public float healthMax = 100f;
        public float health = 100f;


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

        void Update()
        {
            if (health > 0)
            {
                float yPosition = Mathf.Sin(GameController.instance.timeElapsed) * 3f;
                transform.position = new Vector2(0, yPosition);
            }
        }

        void Die()
        {
            StopCoroutine("FiringTimer");
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            if (health <= 0)
            {
                Die();
            }
            imageHealthBar.fillAmount = health / healthMax;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Asteroid>())
            {
                TakeDamage(10f);
            }
        }

        public void RepairHull()
        {
            int cost = 100;
            if (GameController.instance.money >= cost && health < healthMax && health > 0)
            {
                GameController.instance.money -= cost;

                health = healthMax;
                imageHealthBar.fillAmount = health / healthMax;
            }
        }

        public void UpgradeHull()
        {
            int cost = Mathf.RoundToInt(healthMax);

            if (GameController.instance.money >= cost)
            {
                GameController.instance.money -= cost;

                health += 50;
                healthMax += 50;
                imageHealthBar.fillAmount = health / healthMax;

                hullUpgradeText.text = "Hull Strength $" + Mathf.RoundToInt(healthMax);
            }
        }

        public void UpgradeFireSpeed()
        {
            int cost = 100 + Mathf.RoundToInt((1f - firingDelay) * 100f);

            if (GameController.instance.money >= cost)
            {
                GameController.instance.money -= cost;

                firingDelay -= 0.05f;

                int newCost = 100 + Mathf.RoundToInt((1f - firingDelay) * 100f);
                fireSpeedUpgradeText.text = "Fire Speed $" + newCost;
            }
        }


    }
}
