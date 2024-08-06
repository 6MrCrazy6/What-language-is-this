using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using TMPro;

namespace Setting
{
    public class GooglePlayAuth : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerNameText;
        [SerializeField] private Button loginButton;


        private void Start()
        {
            if (PlayerPrefs.GetInt("GooglePlayAuthEnabled") == 1)
            {
                AuthenticateUser();
            }
            else
            {
                UpdateUI(false);
            }

            loginButton.onClick.AddListener(AuthenticateUser);
        }

        private void AuthenticateUser()
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Debug.Log("Google Play Services Authenticated");
                    PlayerPrefs.SetInt("GooglePlayAuthEnabled", 1);
                    UpdateUI(true);
                }
                else
                {
                    Debug.Log("Failed to authenticate with Google Play Services");
                    PlayerPrefs.SetInt("GooglePlayAuthEnabled", 0);
                    UpdateUI(false);
                }
            });
        }


        private void UpdateUI(bool isAuthenticated)
        {
            if (isAuthenticated)
            {
                playerNameText.text = Social.localUser.userName;
                loginButton.interactable = false;
                Color buttonColor = loginButton.image.color;
                buttonColor.a = 0.5f;  
                loginButton.image.color = buttonColor;
            }
            else
            {
                playerNameText.text = "";
                loginButton.interactable = true;
                Color buttonColor = loginButton.image.color;
                buttonColor.a = 1f;
                loginButton.image.color = buttonColor;
            }
        }
    }

}
