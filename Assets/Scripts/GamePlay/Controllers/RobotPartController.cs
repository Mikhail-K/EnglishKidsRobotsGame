using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EnglishKids.RobotsConstructor
{
    public class RobotPartController : MonoBehaviour
    {
        public RobotPartView robotPartView;
        public RobotPart robotPart;

        public event Action<RobotPartController> onMounted;
        private void Awake()
        {
            if (robotPart == null)
                robotPart = new RobotPart();
            if (robotPartView == null)
                robotPartView = GetComponent<RobotPartView>();
            robotPartView.onMounted += OnMounted;
        }

        public void OnMounted()
        {
            robotPart.Mounted = true;
            onMounted?.Invoke(this);
        }

        public bool CheckIfMounted()
        {
            return robotPart.Mounted;
        }

        public void DestroyPart()
        {
            Destroy(gameObject);
        }
    }
}