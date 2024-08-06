using TMPro;
using UnityEngine;

namespace Localization
{
    public class MainMenuUI : MonoBehaviour, ILocalizable
    {
        [SerializeField] private TextMeshProUGUI playText;
        [SerializeField] private TextMeshProUGUI exitText;
        [SerializeField] private TextMeshProUGUI settingsText;
        [SerializeField] private TextMeshProUGUI soundSettingsText;
        [SerializeField] private TextMeshProUGUI accountText;
        [SerializeField] private TextMeshProUGUI accountPanelText;
        [SerializeField] private TextMeshProUGUI languageText;
        [SerializeField] private TextMeshProUGUI languagePanelText;
        [SerializeField] private TextMeshProUGUI soundText;
        [SerializeField] private TextMeshProUGUI soundPanelText;
        [SerializeField] private TextMeshProUGUI musicText;
        [SerializeField] private TextMeshProUGUI backText;
        [SerializeField] private TextMeshProUGUI englishText;
        [SerializeField] private TextMeshProUGUI germanText;
        [SerializeField] private TextMeshProUGUI polishText;
        [SerializeField] private TextMeshProUGUI turkishText;
        [SerializeField] private TextMeshProUGUI signInGmailText;
        [SerializeField] private TextMeshProUGUI signInGooglePlayText;

        private void Start()
        {
            UpdateLocalization();
        }

        public void UpdateLocalization()
        {
            var localization = FindObjectOfType<LocalizationManager>().localizationData;
            playText.text = localization.play;
            exitText.text = localization.exit;
            settingsText.text = localization.settings;
            soundSettingsText.text = localization.sound_settings;
            accountText.text = localization.account;
            accountPanelText.text = localization.account_panel;
            languageText.text = localization.language;
            languagePanelText.text = localization.language_panel;
            soundText.text = localization.sound;
            soundPanelText.text = localization.sound_panel;
            musicText.text = localization.music;
            backText.text = localization.back;
            englishText.text = localization.english;
            germanText.text = localization.german;
            polishText.text = localization.polish;
            turkishText.text = localization.turkish;
            signInGmailText.text = localization.sign_in_gmail;
            signInGooglePlayText.text = localization.sign_in_google_play;
        }
    }
}
