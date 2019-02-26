using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project56
{
    public class GooglePlayManager : SingletonMonoBehaviour<GooglePlayManager>
    {
        public Text StatusText;
        public GameObject Canvas;
        private String DefaultLeaderboard = "CgkI_YSshJYFEAIQBg";

        private void Start()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            // registers a callback to handle game invitations received while the game is not running.
            //.WithInvitationDelegate(< callback method >)
            // requests the email address of the player be available.
            // Will bring up a prompt for consent.
            .RequestEmail()
            // requests a server auth code be generated so it can be passed to an
            //  associated back end server application and exchanged for an OAuth token.
            .RequestServerAuthCode(false)
            // requests an ID token be generated.  This OAuth token can be used to
            //  identify the player to other services such as Firebase.
            .RequestIdToken()
            .Build();

            PlayGamesPlatform.InitializeInstance(config);
            // recommended for debugging:
            PlayGamesPlatform.DebugLogEnabled = true;
            // Activate the Google Play Games platform
            PlayGamesPlatform.Activate();
            StatusText.text = "Authenticating";
            Social.localUser.Authenticate(OnAuthenticationComplete);
        }

        private void OnEnable()
        {
            MyEventManager.Instance.UpdateState.AddListener(UpdateState);

        }

        private void OnDisable()
        {
            MyEventManager.Instance.UpdateState.RemoveListener(UpdateState);
        }

        private void UpdateState(GameState newGameState)
        {
            if (newGameState == GameState.Game)
            {
                Canvas.SetActive(false);
            }
        }

        private void OnAuthenticationComplete(bool result)
        {
            if (result)
                StatusText.text = "Succesful Integration";
            else
                StatusText.text = "Failed";
        }

        public void UpdateScore(int Score)
        {
            Social.ReportScore(Score, DefaultLeaderboard, OnScoreUpdated);
        }

        private void OnScoreUpdated(bool result)
        {
            if (result)
                Debug.Log("Score Updated Succesfully");
            else
                Debug.Log("Score Updating Failed");

        }
    }
}