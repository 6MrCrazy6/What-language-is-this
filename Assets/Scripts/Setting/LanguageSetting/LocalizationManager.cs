using System.Linq;
using UnityEngine;

namespace Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        public LocalizationData localizationData;
        private string currentLanguage = "en";
        private const string LanguageKey = "language";

        private void Awake()
        {
            // «агружаем сохраненный €зык из PlayerPrefs
            currentLanguage = PlayerPrefs.GetString(LanguageKey, "en");
            LoadLocalization();
        }

        public void LoadLocalization()
        {
            TextAsset file = Resources.Load<TextAsset>("Localization/MainMenu");
            string json = file.text;
            var wrapper = JsonUtility.FromJson<LocalizationWrapper>(json);
            if (wrapper != null)
            {
                var entry = wrapper.localizations.FirstOrDefault(e => e.language == currentLanguage);
                if (entry != null)
                {
                    localizationData = entry.data;
                }
                else
                {
                    Debug.LogError($"язык {currentLanguage} не найден в MainMenu.json");
                }
            }      
            ApplyLocalization();
        }

        public void SetLanguage(string language)
        {
            currentLanguage = language;
            PlayerPrefs.SetString(LanguageKey, language);
            PlayerPrefs.Save();

            LoadLocalization();
        }

        public string GetCurrentLanguage()
        {
            return currentLanguage;
        }

        public void ApplyLocalization()
        {
            foreach (var localizable in FindObjectsOfType<MonoBehaviour>().OfType<ILocalizable>())
            {
                localizable.UpdateLocalization();
            }
        }
    }
}

