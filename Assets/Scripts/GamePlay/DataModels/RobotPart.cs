using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.RobotsConstructor
{
    [System.Serializable]
    public class RobotPart
    {
        public bool Mounted { get; set; }

        public Robot ParentRobot { get; set; }
    }
}