using Facebook.MiniJSON;
using Facebook.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AlyxAdventure
{
    public class FacebookManager : MonoBehaviour
    {
        public TextMeshProUGUI Status;

        private void OnEnable()
        {
            MyEventManager.Instance.LoginWithFacebook.AddListener(LoginWithFacebook);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.LoginWithFacebook.RemoveListener(LoginWithFacebook);
            }
        }

        private void Awake()
        {
            if (!FB.IsInitialized)
            {
                Debug.Log("Facebook not Initialized");
                // Initialize the Facebook SDK
                FB.Init(InitCallback, OnHideUnity);
            }
            else
            {
                // Already initialized, signal an app activation App Event
                Debug.Log("Facebook already Initialized");
                FB.ActivateApp();
            }
        }

        private void Start()
        {

        }

        private void InitCallback()
        {
            Debug.Log("Init Call back");
            if (FB.IsInitialized)
            {
                // Signal an app activation App Event
                FB.ActivateApp();
                Debug.Log("Activating app");
            }
            else
            {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }

        private void OnHideUnity(bool isGameShown)
        {
            if (!isGameShown)
            {
                // Pause the game - we will need to hide
                Time.timeScale = 0;
            }
            else
            {
                // Resume the game - we're getting focus again
                Time.timeScale = 1;
            }
        }

        private void LoginWithFacebook()
        {
            StartCoroutine(WaitForInit());

        }

        private IEnumerator WaitForInit()
        {
            while (!FB.IsInitialized)
            {
                yield return null;
            }
            List<string> perms = new List<string>() { "public_profile", "email" };
            FB.LogInWithReadPermissions(perms, AuthCallback);
        }


        private void AuthCallback(ILoginResult result)
        {
            Debug.Log("Auth Call Back");
            if (FB.IsLoggedIn)
            {
                Debug.Log("Fb logged in");
                // AccessToken class will have session details
                AccessToken aToken = AccessToken.CurrentAccessToken;
                Debug.Log("FB Token -" + aToken.UserId);
                // Print current access token's granted permissions
                foreach (string perm in aToken.Permissions)
                {
                    Debug.Log(perm);
                }

                GetUserData();
                MyEventManager.Instance.OnFacebookLogin.Dispatch();
            }
            else
            {
                Debug.Log("User cancelled login");
            }
        }

        private void GetUserData()
        {
            FB.API("me?fields=name,picture", HttpMethod.POST, OnFBDataRecieved);
        }

        private void OnFBDataRecieved(IGraphResult result)
        {
            MyFacebookData myFacebookData = JsonUtility.FromJson<MyFacebookData>(result.RawResult);
            Status.text = "Welcome " + myFacebookData.name;
        }

        [Serializable]
        public class MyFacebookData
        {
            public string name;
            public Picture picture;
        }

        [Serializable]
        public class Picture
        {
            public Data data;
        }

        [Serializable]
        public class Data
        {
            public int height;
            public int width;
            public string url;
        }
    }
}