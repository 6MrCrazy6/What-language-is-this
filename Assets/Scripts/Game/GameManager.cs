using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UI;
using Localization;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private DataLoger dataLoader;

        [SerializeField] private Text questionText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI bestScoreText;
        [SerializeField] private TextMeshProUGUI finishScoreText;
        [SerializeField] private Button[] answerButtons;

        [SerializeField] private RectTransform _TimesUp;
        [SerializeField] private GameObject _BlurBackground;

        [SerializeField] private string[] languageFiles;

        private Dictionary<string, WordData> languageData;
        private Dictionary<string, GameLocalizationData> gameLanguageData;
        private WordData currentLocalization;
        private GameLocalizationData currentGameLocalization; 
        private string currentLanguage;
        private string correctAnswer;

        private int _Score = 0;
        private int _BestScore = 0;

        private float startTime;
        private float duration = 61f;
        private bool timerIsRunning = false;

        private ILocalizationProvider<GameLocalizationData> localizationProvider;

        void Awake()
        {
            _BestScore = PlayerPrefs.GetInt("BestScore");
        }

        private void Start()
        {
            Time.timeScale = 1.0f;

            string[] languageFiles = {
                "Armenian", "Azerbaijan", "Chinese", "Danish", "English", "Esperanto", "French", "German", 
                "Icelandic", "Italian", "Japanese", "Kazakh", "Korean", "Nepali", "Polish", "Portuguese", 
                "Romanian", "Slovak", "Spanish", "Turkish"
            };

            dataLoader.LoadData(languageFiles);

            var loader = new JsonLocalizationLoader("Localization/GameUI");
            localizationProvider = new LocalizationProvider<GameLocalizationData>(loader);

            LoadLocalization();
            StartCoroutine(InitializeGame());
            ResetTimer();
        }

        private void Update()
        {
            if (timerIsRunning)
            {
                float timeElapsed = Time.time - startTime;
                float timeRemaining = duration - timeElapsed;

                if (timeRemaining > 0)
                {
                    UpdateTimerText(timeRemaining);
                }
                else
                {
                    timeRemaining = 0;
                    timerIsRunning = false;
                    UpdateTimerText(timeRemaining);
                    TimeOut();
                }
            }
        }

        private IEnumerator InitializeGame()
        {
            yield return new WaitForSeconds(1);

            languageData = dataLoader.LanguageData;
            GenerateQuestion();
        }

        private void GenerateQuestion()
        {
            JsonLocalizationLoader localizationLoader = new JsonLocalizationLoader("Localization/GameUI");
            var localizations = localizationLoader.LoadLocalization();

            string currentLanguage = PlayerPrefs.GetString("language", "en");

            GameLocalizationData localizationData = null;
            foreach (var localization in localizations)
            {
                if (localization.language == currentLanguage)
                {
                    localizationData = localization.data;
                    break;
                }
            }

            if (localizationData == null) return;

            List<string> languages = new List<string>(languageData.Keys);
            string selectedLanguage = languages[Random.Range(0, languages.Count)];
            WordData wordData = languageData[selectedLanguage];

            correctAnswer = wordData.Words[Random.Range(0, wordData.Words.Count)];
            questionText.text = correctAnswer;
            TextSize.AdjustTextSize(questionText);

            int correctAnswerIndex = localizations[0].data.languageFiles.IndexOf(selectedLanguage);
            string correctAnswerLocalized = localizationData.languageFiles[correctAnswerIndex];

            List<string> answers = new List<string>(languageData.Keys);
            answers.Remove(selectedLanguage);
            answers.Shuffle();
            answers = answers.GetRange(0, answerButtons.Length - 1);
            answers.Add(selectedLanguage);
            answers.Shuffle();

            for (int i = 0; i < answerButtons.Length; i++)
            {
                string answerLanguage = answers[i];
                int answerIndex = localizations[0].data.languageFiles.IndexOf(answerLanguage);
                string answerText = localizationData.languageFiles[answerIndex];

                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answerText;
                answerButtons[i].onClick.RemoveAllListeners();
                if (answerLanguage == selectedLanguage)
                {
                    answerButtons[i].onClick.AddListener(CorrectAnswer);
                }
                else
                {
                    answerButtons[i].onClick.AddListener(WrongAnswer);
                }
            }
        }

        private void CorrectAnswer()
        {
            _Score++;
            scoreText.text = $"{currentGameLocalization.score}: {_Score:D2}"; 

            GenerateQuestion();
        }

        private void WrongAnswer()
        {
            if (_Score <= 0) _Score = 0;
            else _Score--;
            scoreText.text = $"{currentGameLocalization.score}: {_Score:D2}"; 

            GenerateQuestion();
        }

        private void TimeOut()
        {
            _BlurBackground.SetActive(true);
            _TimesUp.DOAnchorPos(new Vector2(0, 0), 1f, true);

            if (_Score > _BestScore)
            {
                _BestScore = _Score;
                PlayerPrefs.SetInt("BestScore", _Score);
                PlayerPrefs.Save();
            }

            bestScoreText.text = $"{currentGameLocalization.best_score}: {_BestScore:D2}";
            finishScoreText.text = $"{currentGameLocalization.finish_score}: {_Score:D2}";
        }

        private void UpdateTimerText(float timeToDisplay)
        {
            timeToDisplay = Mathf.Max(0, timeToDisplay);

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private void ResetTimer()
        {
            startTime = Time.time;
            timerIsRunning = true;
        }

        private void LoadLocalization()
        {
            string currentLanguage = PlayerPrefs.GetString("language", "en");
            currentGameLocalization = localizationProvider.GetLocalizationData(currentLanguage);
        }
    }
}

