using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnglishKids.RobotsConstructor
{
    public class GameManager : MonoBehaviour
    {
        private Scene gameScene;

        public void LaunchGame(Scene scene)
        {
            gameScene = scene;
        }

        public void CloseGame()
        {
            if (gameScene != null)
                SceneManager.UnloadSceneAsync(gameScene);
            else
                Destroy(gameObject);
        }
    }
}
