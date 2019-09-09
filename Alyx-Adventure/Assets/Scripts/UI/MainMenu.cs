using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AlyxAdventure
{
    public class MainMenu : Menu
    {
        private int total;

        public RectTransform PosFrag, PosColl;
        public TextMeshProUGUI FragmentCount, Collectable;
        public GameObject PanelFrag, PanelColl;
        public Button FbButton;

        public GameObject Leaderboard;
        public GameObject Achievements;
        public GameObject GoogleButton;

        public Image ProfilePicture;
        public TextMeshProUGUI PlayerName;
        public GameObject ProfilePanel;

        public void Play()
        {
            SceneManager.LoadScene(2);
        }

        public void Shop()
        {
        }

        private void OnEnable()
        {
            MyEventManager.Instance.OnFacebookLogin.AddListener(OnFacebookLogin);
            MyEventManager.Instance.OnGoogleLogin.AddListener(OnGoogleLogin);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.OnFacebookLogin.RemoveListener(OnFacebookLogin);
                MyEventManager.Instance.OnGoogleLogin.RemoveListener(OnGoogleLogin);

            }
        }

        private void OnFacebookLogin(FacebookManager.MyFacebookData facebookData)
        {
            FbButton.gameObject.SetActive(false);
            ActivateProfilePanel(facebookData.name);
        }

        private void OnGoogleLogin()
        {
            LeanTween.scale(Leaderboard, Vector3.one, 1f);
            LeanTween.scale(Achievements, Vector3.one, 1f);
            GoogleButton.SetActive(false);
            ActivateProfilePanel(GooglePlayManager.Instance.playerData.PlayerName);


        }
        private void ActivateProfilePanel(string name)
        {
            ProfilePanel.SetActive(true);
            PlayerName.text = name;
            LeanTween.scaleX(PlayerName.transform.parent.gameObject, 1, .8f);
        }

        public void EnableDeath(bool enable)
        {
            MyEventManager.Instance.EnableDeath.Dispatch(enable);
        }

        public void ActivateFbButton()
        {
            FbButton.gameObject.SetActive(true);
        }


        private void Awake()
        {
            PanelFrag.GetComponent<CanvasGroup>().alpha = 0;
            PanelColl.GetComponent<CanvasGroup>().alpha = 0;
            FbButton.gameObject.SetActive(false);
        }

        private void Start()
        {
            Leaderboard.transform.localScale = Vector3.zero;
            Achievements.transform.localScale = Vector3.zero;
            if (PrefManager.Instance.GetBoolPref(PrefManager.PreferenceKey.GoogleLogin))
                GoogleButton.SetActive(false);
            total = PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.TotalFragments, 0);
            int earned = PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.FragmentFromTime, 0);

            if (earned > 0)
            {
                FragmentCount.text = (total + earned) + "";
                PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.FragmentFromTime, 0);
                total += earned;
            }
            else
                FragmentCount.text = total + "";

            CheckFragment();
            CheckCollectible();
            ShowFragments();
            ProfilePanel.SetActive(false);
            ProfilePicture.transform.parent.SetActive(false);
        }


        private void ShowFragments()
        {
            LeanTween.alphaCanvas(PanelFrag.GetComponent<CanvasGroup>(), 1, .5f).setOnComplete(MoveFragToTop);
            LeanTween.alphaCanvas(PanelColl.GetComponent<CanvasGroup>(), 1, .5f).setOnComplete(MoveCollToTop);
        }

        private void CheckFragment()
        {
            if (total >= CollectableManager.Instance.TotalFragFor1Collectable)
            {
                PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.TotalFragments, total % CollectableManager.Instance.TotalFragFor1Collectable);
                FragmentCount.text = total % CollectableManager.Instance.TotalFragFor1Collectable + "";
            }
            else
            {
                PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.TotalFragments, total);
            }
        }

        public void CheckCollectible()
        {
            int tot = PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.TotalCollectables, 0);
            if (total >= CollectableManager.Instance.TotalFragFor1Collectable)
            {
                tot += total / CollectableManager.Instance.TotalFragFor1Collectable;
                PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.TotalCollectables, tot);
            }
            Collectable.text = tot + "";
        }


        private void MoveFragToTop()
        {
            LeanTween.move(PanelFrag.GetComponent<RectTransform>(), PosFrag.anchoredPosition, .2f);
        }

        private void MoveCollToTop()
        {
            LeanTween.move(PanelColl.GetComponent<RectTransform>(), PosColl.anchoredPosition, .2f).setOnComplete(ActivateFbButton);
        }

        public void LoginWithFacebook()
        {
            MyEventManager.Instance.LoginWithFacebook.Dispatch();
        }

        public void LoginWithGoogle()
        {
            MyEventManager.Instance.LoginWithGoogle.Dispatch();
        }

        public void ShowAchievements()
        {
            MyEventManager.Instance.ShowAchievements.Dispatch();
        }

        public void ShowLeaderboard()
        {
            MyEventManager.Instance.ShowLeaderboard.Dispatch();

        }


    }
}