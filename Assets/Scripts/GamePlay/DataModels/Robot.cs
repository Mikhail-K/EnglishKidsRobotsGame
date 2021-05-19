using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.RobotsConstructor
{
    public class Robot : MonoBehaviour
    {
        public string robotName;
        public string robotColor;

        public Texture2D leftBackground;
        public Texture2D rightBackground;

        public Texture2D GetLeftImage()
        {
            return leftBackground;
        }

        public Texture2D GetRightImage()
        {
            return rightBackground;
        }
    }
}
