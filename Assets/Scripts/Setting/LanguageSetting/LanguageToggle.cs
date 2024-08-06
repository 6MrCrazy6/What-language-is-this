using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    public class LanguageToggle : MonoBehaviour
    {
        [SerializeField] private Toggle englishToggle;
        [SerializeField] private Toggle germanToggle;
        [SerializeField] private Toggle polishToggle;
        [SerializeField] private Toggle turkishToggle;

        private void Start()
        {
            englishToggle.onValueChanged.AddListener((isOn) => OnToggleLanguageChanged(isOn, "en"));
            germanToggle.onValueChanged.AddListener((isOn) => OnToggleLanguageChanged(isOn, "de"));
            polishToggle.onValueChanged.AddListener((isOn) => OnToggleLanguageChanged(isOn, "pl"));
            turkishToggle.onValueChanged.AddListener((isOn) => OnToggleLanguageChanged(isOn, "tr"));

            string currentLanguage = FindObjectOfType<LocalizationManager>().GetCurrentLanguage();
            switch (currentLanguage)
            {
                case "en":
                    englishToggle.isOn = true;
                    break;
                case "de":
                    germanToggle.isOn = true;
                    break;
                case "pl":
                    polishToggle.isOn = true;
                    break;
                case "tr":
                    turkishToggle.isOn = true;
                    break;
            }
        }

        private void OnToggleLanguageChanged(bool isOn, string language)
        {
            if (isOn)
            {
                FindObjectOfType<LocalizationManager>().SetLanguage(language);
                SetAllTogglesOffExcept(language);
            }
        }

        private void SetAllTogglesOffExcept(string language)
        {
            if (language != "en") englishToggle.isOn = false;
            if (language != "de") germanToggle.isOn = false;
            if (language != "pl") polishToggle.isOn = false;
            if (language != "tr") turkishToggle.isOn = false;
        }
    }
}


