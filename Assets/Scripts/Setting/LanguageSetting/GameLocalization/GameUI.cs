using TMPro;
using UnityEngine;

namespace Localization
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pauseText;
        [SerializeField] private TextMeshProUGUI resumeText;
        [SerializeField] private TextMeshProUGUI exitText;
        [SerializeField] private TextMeshProUGUI timesUpText;
        [SerializeField] private TextMeshProUGUI bestScoreText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI finishScoreText;
        [SerializeField] private TextMeshProUGUI restartText;
        [SerializeField] private TextMeshProUGUI exitFinshText;

        private ILocalizationProvider<GameLocalizationData> localizationProvider;
        private GameLocalizationData currentLocalization;

        private void Awake()
        {
            var loader = new JsonLocalizationLoader("Localization/GameUI");
            localizationProvider = new LocalizationProvider<GameLocalizationData>(loader);
            LoadLocalization();
        }

        private void LoadLocalization()
        {
            string currentLanguage = PlayerPrefs.GetString("language", "en");
            currentLocalization = localizationProvider.GetLocalizationData(currentLanguage);
            UpdateUIText();
        }

        private void UpdateUIText()
        {
            scoreText.text = currentLocalization.score;
            pauseText.text = currentLocalization.pause;
            resumeText.text = currentLocalization.resume;
            exitText.text = currentLocalization.exit;
            timesUpText.text = currentLocalization.times_up;
            bestScoreText.text = currentLocalization.best_score;
            finishScoreText.text = currentLocalization.finish_score;
            restartText.text = currentLocalization.restart;
            exitFinshText.text = currentLocalization.exit;
        }
    }
}

