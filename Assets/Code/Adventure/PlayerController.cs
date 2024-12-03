using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{
    public class PlayerController : MonoBehaviour
    {

        // Outlets
        Rigidbody2D _rigidbody;
        Animator _animator;

        // Configuration
        public KeyCode keyUp;
        public KeyCode keyDown;
        public KeyCode keyLeft;
        public KeyCode keyRight;
        public float moveSpeed;

        // Methods
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
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
    }
}

