using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{
    public enum Direction
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }

    public class PlayerController : MonoBehaviour
    {

        // Outlets
        Rigidbody2D _rigidbody;
        Animator _animator;
        SpriteRenderer _spriteRenderer;
        public Transform[] attackZones;

        // Configuration
        public KeyCode keyUp;
        public KeyCode keyDown;
        public KeyCode keyLeft;
        public KeyCode keyRight;
        public float moveSpeed;
        public Sprite[] sprites;

        // State Tracking
        public Direction facingDirection;

        // Methods
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        void Update()
        {
            float movementSpeed = _rigidbody.velocity.sqrMagnitude;
            _animator.SetFloat("speed", movementSpeed);

            if (movementSpeed > 0.1f)
            {
                _animator.SetFloat("movementX", _rigidbody.velocity.x);
                _animator.SetFloat("movementY", _rigidbody.velocity.y);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetTrigger("attack");

                // Convert the enumeration to an index
                int facingDirectionIndex = (int)facingDirection;

                // Get an attack zone from index
                Transform attackZone = attackZones[facingDirectionIndex];

                // What objects are within a circle at that attack zone?
                Collider2D[] hits = Physics2D.OverlapCircleAll(attackZone.position, 0.1f);

                // Handle each hit target
                foreach (Collider2D hit in hits)
                {
                    Breakable breakableObject = hit.GetComponent<Breakable>();
                    if (breakableObject)
                    {
                        breakableObject.Break();
                    }
                }
            }
        }


        void FixedUpdate()
        {
            if (Input.GetKey(keyUp))
            {
                _rigidbody.AddForce(Vector2.up * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
            if (Input.GetKey(keyDown))
            {
                _rigidbody.AddForce(Vector2.down * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
            if (Input.GetKey(keyLeft))
            {
                _rigidbody.AddForce(Vector2.left * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
            if (Input.GetKey(keyRight))
            {
                _rigidbody.AddForce(Vector2.right * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
        }
        void LateUpdate()
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if (_spriteRenderer.sprite == sprites[i])
                {
                    facingDirection = (Direction)i;
                    break;
                }
            }
        }

    }
}

