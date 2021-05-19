using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.RobotsConstructor
{
    public class AudioManager : MonoBehaviour
    {
        public List<AudioClip> audioClips = new List<AudioClip>();

        public AudioSource audioSource;

        public virtual void PlaySound(string soundName)
        {
            var clip = audioClips.Find(x => x.name == soundName);
            PlayAudioClip(clip);
        }

        public void PlayAudioClip(AudioClip clip)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.clip = null;
            }

            audioSource.clip = clip;
            audioSource.Play();
        }

        public void SetVolume(float value)
        {
            audioSource.volume = value;
        }
    }

    [System.Serializable]
    public class ColorSound
    {
        public string name;
        public AudioClip audioClip;
    }
}