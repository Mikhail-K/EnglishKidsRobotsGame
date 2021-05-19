using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EnglishKids.RobotsConstructor
{
    public class RobotView : MonoBehaviour
    {
        public Image silhouette;

        public List<GameObject> parts;

        public RectTransform mainPartsContainer;

        public SkeletonGraphic robotAnimation;

        public List<GameObject> Parts { get { return parts; } }

        private void Awake()
        {
            foreach (var part in parts)
            {
                part.GetComponent<RobotPartView>().GenerateSlot();
                part.gameObject.SetActive(false);
            }
        }

        public void SetActiveRobotAnimation(bool active)
        {
            if (robotAnimation)
                robotAnimation.gameObject.SetActive(active);
        }

        public void SetActiveMainBody(bool active)
        {
            silhouette.enabled = active;
            mainPartsContainer.gameObject.SetActive(active);
        }
    }
}
