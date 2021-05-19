using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.RobotsConstructor
{
    public class MusicManager : AudioManager
    {
        private void Start()
        {
            PlayRandomMusic();
        }

        public void PlayRandomMusic()
        {
            int rnd = Random.Range(0, audioClips.Count);
            var clip = audioClips[rnd];
            audioSource.clip = clip;
            audioSource.Play();
        }

        private void Update()
        {
            if (!audioSource.isPlaying)
            {
                PlayRandomMusic();
            }
        }
    }
}