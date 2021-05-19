using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EnglishKids.RobotsConstructor
{
    public class ColorSoundPanel : MonoBehaviour
    {
        public Text colorName;

        public event Action<string> onPlaySound;

        public void SetColorName(string text)
        {
            colorName.text = text;
            name = text;
        }

        public void OnPlayButton()
        {
            onPlaySound?.Invoke(colorName.text);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
