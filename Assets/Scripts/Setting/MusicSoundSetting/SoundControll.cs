using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Setting
{
    public class SoundControll : MonoBehaviour
    {
        [SerializeField] private AudioSource soundEffect;
        [SerializeField] private AudioClip clickClip;
        [SerializeField] private Slider soundSlider;

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

        void Update()
        {
            if (soundEffect != null)
            {
                soundEffect.volume = PlayerPrefs.GetFloat(soundVolumeKey);
            }
            soundSlider.value = PlayerPrefs.GetFloat(soundVolumeKey);
            PlayerPrefs.SetFloat(soundVolumeKey, soundSlider.value);
            PlayerPrefs.Save();
        }

        public void OnSliderValueChanged()
        {
            if (soundEffect != null)
            {
                soundEffect.volume = soundSlider.value;
            }
            PlayerPrefs.SetFloat(soundVolumeKey, soundSlider.value);
            PlayerPrefs.Save();
        }

        public void StartButton()
        {
            StartCoroutine(PlaySoundThenAct(() => SceneManager.LoadScene("Game")));
        }

        public void ExitButton()
        {
            StartCoroutine(PlaySoundThenAct(Application.Quit));
        }

        public void BackButton(GameObject canvas)
        {
            StartCoroutine(PlaySoundThenAct(() => {
                canvas.SetActive(false);
            }));
        }

        public void OtherButton(GameObject newCanvas)
        {
            StartCoroutine(PlaySoundThenAct(() => {
                newCanvas.SetActive(true);
            }));
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
    }
}