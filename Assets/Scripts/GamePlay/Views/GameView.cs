using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine;
using Spine.Unity;

namespace EnglishKids.RobotsConstructor
{
    public class GameView : MonoBehaviour
    {
        public RawImage leftImage;
        public RawImage rightImage;

        public RectTransform leftRobotPosition;
        public RectTransform rightRobotPosition;

        public SkeletonGraphic starsAnimation;

        private void Start()
        {
            InitStarAnimation();
        }

        public void SetLeftRobot(RobotView robotView, Texture2D texture)
        {
            robotView.transform.SetParent(leftRobotPosition, false);
            robotView.transform.position = leftRobotPosition.position;
            leftImage.texture = texture;
        }

        public void SetRightRobot(RobotView robotView, Texture2D texture)
        {
            robotView.transform.SetParent(rightRobotPosition, false);
            robotView.transform.position = rightRobotPosition.position;
            rightImage.texture = texture;
        }

        public void ShowStarAnimation(Vector3 position)
        {
            starsAnimation.transform.position = position;
            starsAnimation.gameObject.SetActive(true);
            starsAnimation.AnimationState.SetAnimation(0, "correct_answer", false);
        }

        private void InitStarAnimation()
        {
            starsAnimation.AnimationState.Complete += OnStarAnimationComplete;
        }

        private void OnStarAnimationComplete(TrackEntry trackEntry)
        {
            starsAnimation.gameObject.SetActive(false);
        }
    }
}
