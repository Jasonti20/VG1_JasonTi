using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        // Outlets
        AudioSource audioSource;
        public AudioClip missSound;
        public AudioClip hitSound;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySoundHit()
        {
            audioSource.PlayOneShot(hitSound);
        }

        public void PlaySoundMiss()
        {
            audioSource.PlayOneShot(missSound);
        }

    }
}

