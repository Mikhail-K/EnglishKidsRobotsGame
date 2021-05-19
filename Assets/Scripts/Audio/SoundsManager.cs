using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.RobotsConstructor
{
    public class SoundsManager : AudioManager
    {
        private static SoundsManager inst;

        private void Awake()
        {
            inst = this;
        }

        public static void Play(string name)
        {
            inst.PlaySound(name);
        }
    }
}
