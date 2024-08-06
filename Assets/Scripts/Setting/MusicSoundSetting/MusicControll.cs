using UnityEngine;
using UnityEngine.UI;

namespace Setting
{
    public class MusicControll : MonoBehaviour
    {
        private AudioSource music;
        public Slider slider;
        private const string VolumePrefsKey = "Volume";

        private void Awake()
        {
            if (!PlayerPrefs.HasKey(VolumePrefsKey))
            {
                PlayerPrefs.SetFloat(VolumePrefsKey, 1f);
            }
            else
            {
                GameObject musicPlayer = GameObject.FindGameObjectWithTag("Music");
                music = musicPlayer.GetComponent<AudioSource>();
                music.volume = PlayerPrefs.GetFloat(VolumePrefsKey);
            }
        }

        void Update()
        {
            music.volume = PlayerPrefs.GetFloat(VolumePrefsKey);
            slider.value = PlayerPrefs.GetFloat(VolumePrefsKey);
            PlayerPrefs.SetFloat(VolumePrefsKey, music.volume);
            PlayerPrefs.Save();
        }

        public void OnSliderValueChanged()
        {
            music.volume = slider.value;
            PlayerPrefs.SetFloat(VolumePrefsKey, music.volume);
            PlayerPrefs.Save();
        }
    }
}

