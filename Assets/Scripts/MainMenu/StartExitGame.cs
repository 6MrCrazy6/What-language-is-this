using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class StartExitGame : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("Game");
        }

        public void ExitGame()
        {
            Application.Quit();
            Debug.Log("Game close");
        }
    }
}

