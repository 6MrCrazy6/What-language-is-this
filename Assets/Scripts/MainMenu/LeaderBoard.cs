using GooglePlayGames;
using UnityEngine;

namespace MainMenu
{
    public class LeaderBoard : MonoBehaviour
    {
        [HideInInspector]
        private const string leaderboard = "CgkI4dCbhOsSEAIQAQ";

        private void Start()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate(success =>
            {
                if (success)
                {
                    Debug.Log("User authenticate");
                    AddScore();
                }
                else
                {
                    Debug.Log("User not authenticate");
                }
            });
        }

        void AddScore()
        {
            int bestScore = PlayerPrefs.GetInt("BestScore");
            Debug.Log(bestScore);
            Social.ReportScore(bestScore, leaderboard, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Score submitted to Leaderboard successfully");
                }
                else
                {
                    Debug.Log("Score submission to Leaderboard failed");
                }
            });
        }

        public void ShowLeaderBoard()
        {
            Social.ShowLeaderboardUI();
        }
    }
}

