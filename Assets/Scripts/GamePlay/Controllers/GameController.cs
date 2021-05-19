using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.RobotsConstructor
{
    public class GameController : MonoBehaviour
    {
        public RobotsFactory robotsFactory;
        public TransporterController transporterController;
        
        public GameView gameVeiw;
        public GameMenu gameMenu;

        public bool launchGameOnStart;

        private void Start()
        {
            Subscribe();

            if (launchGameOnStart)
                LaunchGame();
        }

        private void Subscribe()
        {
            transporterController.currentPartsMounted += OnCompleteAllCurrentRobotParts;
            transporterController.onePartMounted += OnPartMounted;
            transporterController.allPartsMounted += OnCompleteGame;

            gameMenu.onPlayColorSound += OnPlayColorNameSound;
            gameMenu.onRestartGame += RestartGame;
        }

        private void Unsubscribe()
        {
            if (transporterController)
            {
                transporterController.currentPartsMounted -= OnCompleteAllCurrentRobotParts;
                transporterController.onePartMounted -= OnPartMounted;
            }

            if(gameMenu)
                gameMenu.onPlayColorSound -= OnPlayColorNameSound;
        }

        [ContextMenu("LaunchGame")]
        public void LaunchGame()
        {
            RobotController r1 = robotsFactory.GetRandomRobot();
            RobotController r2 = null;
            if (r1 != null)
            {
                r2 = robotsFactory.GetRandomRobot(r1.GetRobotName());
            }

            if (r1 == null || r2 == null)
            {
                Debug.LogError("Error create robots");
                return;
            }

            r1.gameObject.SetActive(true);
            r2.gameObject.SetActive(true);

            gameVeiw.SetLeftRobot(r1.RobotView, r1.Robot.GetLeftImage());
            gameVeiw.SetRightRobot(r2.RobotView, r2.Robot.GetRightImage());

            transporterController.PutRobotPartsOnLine(new List<RobotController>() { r1, r2 });
            transporterController.LaunchTransporter();

            gameMenu.SetLeftColorSoundPanelText(r1.GetRobotColorName());
            gameMenu.SetRightColorSoundPanelText(r2.GetRobotColorName());
        }

        public void OnCompleteAllCurrentRobotParts()
        {
            transporterController.ContinueTransporter();
        }

        public void RestartGame()
        {
            gameMenu.SetActiveRestartButton(false);
            ClearGame();
            LaunchGame();
        }

        [ContextMenu("ClearGame")]
        private void ClearGame()
        {
            transporterController.ClearTransporter();
            gameMenu.SetActiveRightColorSoundPanel(false);
            gameMenu.SetActiveLeftColorSoundPanel(false);
        }

        private void OnPartMounted(RobotPartController rpc)
        {
            string color = rpc.robotPart.ParentRobot.robotColor;
            gameMenu.SetActiveColorSoundPanel(color, true);

            ColorSoundsManager.Play(color);
            gameVeiw.ShowStarAnimation(rpc.transform.position);
        }

        private void OnPlayColorNameSound(string color)
        {
            ColorSoundsManager.Play(color);
        }

        [ContextMenu("TestContinueTransporter")]
        private void TestContinueTransporter()
        {
            transporterController.TestClearLine();
            OnCompleteAllCurrentRobotParts();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void OnCompleteGame()
        {
            gameMenu.SetActiveRestartButton(true);
        }
    }
}
