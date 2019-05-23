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

        private IEnumerator Start()
        {
            //StatusText.text = "Running Google Play";

            //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            ////.EnableSavedGames()
            ////// registers a callback to handle game invitations received while the game is not running.
            //////.WithInvitationDelegate(< callback method >)
            ////// requests the email address of the player be available.
            ////// Will bring up a prompt for consent.
            ////.RequestEmail()

            ////// requests a server auth code be generated so it can be passed to an
            //////  associated back end server application and exchanged for an OAuth token.
            ////.RequestServerAuthCode(false)
            ////// requests an ID token be generated.  This OAuth token can be used to
            //////  identify the player to other services such as Firebase.
            ////.RequestIdToken()
            //.Build();
            //StatusText.text = "Play Build done";

            //PlayGamesPlatform.InitializeInstance(config);
            //StatusText.text = "Play Initialized";

            //// recommended for debugging:
            //PlayGamesPlatform.DebugLogEnabled = true;
            //// Activate the Google Play Games platform
            //PlayGamesPlatform.Activate();
            //StatusText.text = "Actrivate called";
            yield return new WaitForSeconds(1f);
            //StatusText.text = "Authenticating";
            //Login();
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

        public void ShowDefaultLeaderBoards(string LeaderboardId = null)
        {
            if (string.IsNullOrEmpty(LeaderboardId))
                PlayGamesPlatform.Instance.ShowLeaderboardUI();
            else
                PlayGamesPlatform.Instance.ShowLeaderboardUI(LeaderboardId);
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