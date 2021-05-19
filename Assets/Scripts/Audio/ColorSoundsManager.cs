using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace EnglishKids.RobotsConstructor
{
    public class ColorSoundsManager : AudioManager
    {
        public List<ColorSound> colorSounds;

        //private Queue<AudioClip> soundsQueue = new Queue<AudioClip>();
        private AudioClip lastClip;

        private static ColorSoundsManager inst;

        private void Awake()
        {
            inst = this;
        }

        public static void Play(string colorName)
        {
            inst.PlaySound(colorName);
        }

        public override void PlaySound(string soundName)
        {
            var sound = colorSounds.Find(x => x.name == soundName);
            if (sound == null)
            {
                Debug.LogError("No color sound " + soundName);
            }

            if (!audioSource.isPlaying)
            {
                PlayAudioClip(sound.audioClip);
                lastClip = null;
            }
            else
            {
                lastClip = sound.audioClip;
            }
            /*if (!audioSource.isPlaying && soundsQueue.Count <= 0)
            {
                PlayAudioClip(sound.audioClip);
            }
            else
            {
                soundsQueue.Enqueue(sound.audioClip);
                if(soundsQueue.Any(x => x != sound.audioClip))
                    soundsQueue = new Queue<AudioClip>(soundsQueue.Where(x => x == sound.audioClip));
            }*/
        }

        private void Update()
        {

            if (!audioSource.isPlaying && lastClip != null)
            {
                PlayAudioClip(lastClip);
                lastClip = null;
            }
            /*if (!audioSource.isPlaying && soundsQueue.Count > 0)
            {
                var clip = soundsQueue.Dequeue();
                PlayAudioClip(clip);
            }*/
        }
    }
}
