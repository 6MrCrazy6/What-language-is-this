using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _PauseMenu;
        [SerializeField] private GameObject _BlurBackground;
        [SerializeField] private AudioSource _TimerAudioSource; 

        private float _audioTime;

        public void Pause()
        {
            _PauseMenu.SetActive(true);
            _BlurBackground.SetActive(true);
            Time.timeScale = 0f;

            _audioTime = _TimerAudioSource.time;
            _TimerAudioSource.Pause();

            Debug.Log($"Paused at {_audioTime}");
        }

        public void Resume()
        {
            _PauseMenu.SetActive(false);
            _BlurBackground.SetActive(false);
            Time.timeScale = 1.0f;

            _TimerAudioSource.time = _audioTime;
            _TimerAudioSource.Play();

            Debug.Log($"Resumed from {_audioTime}");
        }

        public void BackToMenu()
        {
            Time.timeScale = 1.0f; 
            SceneManager.LoadScene("MainMenu");
        }
    }
}

