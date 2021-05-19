using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.RobotsConstructor
{
    public class AudioController : MonoBehaviour
    {
        public AudioManager soundsManager;
        public AudioManager colorSoundsManager;
        public AudioManager musicManager;

        public float soundsVolume = 0.3f;
        public float colorSoundsVolume = 1f;
        public float musicVolume = 0.2f;

        private void Start()
        {
            UpdateVolume();
        }

        public void UpdateVolume()
        {
            soundsManager.SetVolume(soundsVolume);
            colorSoundsManager.SetVolume(colorSoundsVolume);
            musicManager.SetVolume(musicVolume);
        }
    }
}
