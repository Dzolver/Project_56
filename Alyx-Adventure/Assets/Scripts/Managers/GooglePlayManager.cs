using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace AlyxAdventure
{
    public class GooglePlayManager : SingletonMonoBehaviour<GooglePlayManager>
    {
        public TextMeshProUGUI StatusText;
        public GameObject Canvas;
        private readonly string DefaultLeaderboard = "CgkI_YSshJYFEAIQBg";
        public PlayerData playerData;

        private void Start()
        {
            ActivatePlayGames();
        }

        void ActivatePlayGames()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            //// registers a callback to handle game invitations received while the game is not running.
            ////.WithInvitationDelegate(< callback method >)
            //// requests the email address of the player be available.
            //// Will bring up a prompt for consent.
            .RequestEmail()

            //// requests a server auth code be generated so it can be passed to an
            ////  associated back end server application and exchanged for an OAuth token.
            .RequestServerAuthCode(false)
            //// requests an ID token be generated.  This OAuth token can be used to
            ////  identify the player to other services such as Firebase.
            .RequestIdToken()
            .Build();

            PlayGamesPlatform.InitializeInstance(config);
            // recommended for debugging:
            PlayGamesPlatform.DebugLogEnabled = true;
            // Activate the Google Play Games platform
            PlayGamesPlatform platform = PlayGamesPlatform.Activate();
            if (PrefManager.Instance.GetBoolPref(PrefManager.PreferenceKey.GoogleLogin))
                LoginWithGoogle();
        }

        private void OnEnable()
        {
            MyEventManager.Instance.LoginWithGoogle.AddListener(LoginWithGoogle);
            MyEventManager.Instance.ShowAchievements.AddListener(ShowAchievements);
            MyEventManager.Instance.ShowLeaderboard.AddListener(ShowDefaultLeaderBoard);
        }

        private void OnDisable()
        {
            MyEventManager.Instance.LoginWithGoogle.RemoveListener(LoginWithGoogle);
            MyEventManager.Instance.ShowAchievements.RemoveListener(ShowAchievements);
            MyEventManager.Instance.ShowLeaderboard.RemoveListener(ShowDefaultLeaderBoard);
        }

        private void LoginWithGoogle()
        {
            Social.localUser.Authenticate(OnAuthenticationComplete);
        }

        private void OnAuthenticationComplete(bool result,string message)
        {
            PrefManager.Instance.UpdateBoolpref(PrefManager.PreferenceKey.GoogleLogin, result);
            if (result)
            {
                playerData = new PlayerData();
                playerData.PlayerName = Social.localUser.userName;
                playerData.PlayerID = Social.localUser.id;
                MyEventManager.Instance.OnGoogleLogin.Dispatch();
            }
            else
                StatusText.text = "Try Again\n" + message;
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


        public void Login()
        {
            Social.localUser.Authenticate(OnAuthenticationComplete);
        }

        public void Logout()
        {
            PlayGamesPlatform.Instance.SignOut();
        }

        public bool IsSignedIn()
        {
            return PlayGamesPlatform.Instance.localUser.authenticated;
        }

        public void AddToLeaderBoard(long score, string board, Action OnSuccess, Action OnError)
        {
            if (IsSignedIn())
            {
                PlayGamesPlatform.Instance.ReportScore(score, board, (bool success) =>
                {
                    if (success)
                        OnSuccess();
                    else
                        OnError();
                });
            }
        }

        public void AddToLeaderBoard(long score, string board, string tag, Action OnSuccess, Action OnError)
        {
            if (IsSignedIn())
            {
                PlayGamesPlatform.Instance.ReportScore(score, board, tag, (bool success) =>
                {
                    if (success)
                        OnSuccess();
                    else
                        OnError();
                });
            }
        }

        private void ShowAchievements()
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }

        private void ShowDefaultLeaderBoard()
        {
                PlayGamesPlatform.Instance.ShowLeaderboardUI(DefaultLeaderboard);
        }

        public void GetLeaderBoardsAroundPlayer(string leaderboardId, int rowcount, Action<LeaderboardScoreData> OnFetched,
                                                 LeaderboardCollection collection = LeaderboardCollection.Public,
                                                 LeaderboardTimeSpan timeSpan = LeaderboardTimeSpan.AllTime)
        {
            if (IsSignedIn())
            {
                PlayGamesPlatform.Instance.LoadScores(leaderboardId, LeaderboardStart.PlayerCentered,
                rowcount, collection, timeSpan,
                (data) =>
                {
                    OnFetched(data);
                });
            }
        }

        public void GetTopLeaderBoards(string leaderboardId, int rowcount, Action<LeaderboardScoreData> OnFetched,
                                                 LeaderboardCollection collection = LeaderboardCollection.Public,
                                                 LeaderboardTimeSpan timeSpan = LeaderboardTimeSpan.AllTime)
        {
            if (IsSignedIn())
            {
                PlayGamesPlatform.Instance.LoadScores(leaderboardId, LeaderboardStart.TopScores,
                 rowcount, collection, timeSpan,
                (data) =>
                {
                    OnFetched(data);
                });
            }
        }
    }
}