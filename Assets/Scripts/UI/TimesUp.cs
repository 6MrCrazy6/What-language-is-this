using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace UI
{
    public class TimesUp : MonoBehaviour
    {
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            Time.timeScale = 1.0f;
        }

        public void ExitGame()
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }

}
