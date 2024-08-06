using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Setting
{
    public class SoundControllGame : MonoBehaviour
    {
        [SerializeField] private AudioSource soundEffect;
        [SerializeField] private AudioSource timerEffect;
        [SerializeField] private AudioClip clickClip;

        private string soundVolumeKey = "SoundVolume";
        private float delay = 0.5f;

        private void Awake()
        {
            Time.timeScale = 1.0f;

            if (!PlayerPrefs.HasKey(soundVolumeKey))
            {
                PlayerPrefs.SetFloat(soundVolumeKey, 1f);
            }

            GameObject soundPlayer = GameObject.FindGameObjectWithTag("Sound");
            if (soundPlayer != null)
            {
                soundEffect = soundPlayer.GetComponent<AudioSource>();
                if (soundEffect != null)
                {
                    soundEffect.volume = PlayerPrefs.GetFloat(soundVolumeKey);
                }
            }
        }

        private void Start()
        {
            timerEffect.Play();
        }

        void Update()
        {
            soundEffect.volume = PlayerPrefs.GetFloat(soundVolumeKey);
            timerEffect.volume = PlayerPrefs.GetFloat(soundVolumeKey);
        }

        public void BackToMenuButton()
        {
            StartCoroutine(PlaySoundThenAct(() => SceneManager.LoadScene("MainMenu")));
        }

        public void RestartButton()
        {
            StartCoroutine(PlaySoundThenAct(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single)));
            Time.timeScale = 1.0f;
        }

        private IEnumerator PlaySoundThenAct(System.Action action)
        {
            if (soundEffect != null && clickClip != null)
            {
                soundEffect.PlayOneShot(clickClip);
            }

            yield return new WaitForSeconds(delay);

            action.Invoke();
        }

        private IEnumerator PlaySound()
        {
            if (soundEffect != null && clickClip != null)
            {
                soundEffect.PlayOneShot(clickClip);
            }

            yield return new WaitForSeconds(delay);
        }
    }
}

