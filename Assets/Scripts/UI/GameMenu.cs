using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnglishKids.RobotsConstructor
{
    public class GameMenu : MonoBehaviour
    {
        public ColorSoundPanel leftPlayColorVoicePanel;
        public ColorSoundPanel rightPlayColorVoicePanel;

        public GameObject restartButton;

        public event Action<string> onPlayColorSound;
        public event Action onRestartGame;

        private void Start()
        {
            leftPlayColorVoicePanel.onPlaySound += OnPlayColorSound;
            rightPlayColorVoicePanel.onPlaySound += OnPlayColorSound;
        }

        public void SetActiveColorSoundPanel(string colorName, bool active)
        {
            if (leftPlayColorVoicePanel.name.Equals(colorName))
                leftPlayColorVoicePanel.SetActive(active);
            else if (rightPlayColorVoicePanel.name == colorName)
                rightPlayColorVoicePanel.SetActive(active);
        }

        public void SetLeftColorSoundPanelText(string colorName)
        {
            leftPlayColorVoicePanel.SetColorName(colorName);
        }

        public void SetRightColorSoundPanelText(string colorName)
        {
            rightPlayColorVoicePanel.SetColorName(colorName);
        }

        public void SetActiveLeftColorSoundPanel(bool active)
        {
            leftPlayColorVoicePanel.SetActive(active);
        }

        public void SetActiveRightColorSoundPanel(bool active)
        {
            rightPlayColorVoicePanel.SetActive(active);
        }

        public void OnPlayColorSound(string colorName)
        {
            onPlayColorSound?.Invoke(colorName);
        }

        public void SetActiveRestartButton(bool active)
        {
            if(restartButton)
                restartButton.SetActive(active);
        }

        public void OnRestartGame()
        {
            onRestartGame?.Invoke();
        }
    }
}
