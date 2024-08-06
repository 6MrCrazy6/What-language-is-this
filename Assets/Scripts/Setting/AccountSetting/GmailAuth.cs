using System.Threading.Tasks;
using Firebase.Extensions;
using Google;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Setting
{
    public class GmailAuth : MonoBehaviour
    {
        public string GoogleAPI = "647206725729-tmnuum3df3ktbdeqeb1shfqubtjb8q5m.apps.googleusercontent.com";

        private GoogleSignInConfiguration configuration;

        Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
        Firebase.Auth.FirebaseAuth auth;
        Firebase.Auth.FirebaseUser user;

        public TextMeshProUGUI Username, UserEmail;

        public GameObject LoginScreen, ProfileScreen;
        public Button LoginButton;

        private void Awake()
        {
            configuration = new GoogleSignInConfiguration
            {
                WebClientId = GoogleAPI,
                RequestIdToken = true,
            };
        }

        private void Start()
        {
            InitFirebase();
        }

        void InitFirebase()
        {
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        }

        public void GoogleSignInClick()
        {
            GoogleSignIn.Configuration = configuration;
            GoogleSignIn.Configuration.UseGameSignIn = false;
            GoogleSignIn.Configuration.RequestIdToken = true;
            GoogleSignIn.Configuration.RequestEmail = true;

            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticatedFinished);
        }

        void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task)
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Faulted");
            }
            else if (task.IsCanceled)
            {
                Debug.LogError("Cancelled");
            }
            else
            {
                Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);

                auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task => {
                    if (task.IsCanceled)
                    {
                        return;
                    }

                    if (task.IsFaulted)
                    {
                        Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                        return;
                    }

                    user = auth.CurrentUser;

                    Username.text = user.DisplayName;
                    UserEmail.text = user.Email;

                    SetLoginButtonState(false);
                    ProfileScreen.SetActive(true);
                });
            }
        }

        private void SetLoginButtonState(bool state)
        {
            LoginButton.interactable = state;
            Color buttonColor = LoginButton.GetComponent<Image>().color;
            buttonColor.a = state ? 1f : 0.5f;
            LoginButton.GetComponent<Image>().color = buttonColor;
        }
    }
}

