using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SplitScreen
{
    public class CameraController : MonoBehaviour
    {

        // Outlet
        public Transform target;

        // Configuration
        public Vector3 offset;
        public float smoothness;

        // State Tracking
        Vector3 _velocity;

        void Start()
        {
            if (target)
            {
                offset = transform.position - target.position;
            }
        }

        void FixedUpdate()
        {
            if (target)
            {
                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    target.position + offset,
                    ref _velocity,
                    smoothness
                );
            }
        }


    }
}

