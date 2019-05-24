using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlyxAdventure
{
    public class MainMenu : Menu
    {
        private int total;

        public RectTransform PosFrag, PosColl;
        public TextMeshProUGUI FragmentCount, Collectable;
        public GameObject PanelFrag, PanelColl;

        public void Play()
        {
            HideMenu();
            SceneManager.LoadScene(2);
        }

        public void Leaderboard()
        {
        }

        public void Shop()
        {
        }

        public void Achievements()
        {
        }

        private void Awake()
        {
            PanelFrag.GetComponent<CanvasGroup>().alpha = 0;
            PanelColl.GetComponent<CanvasGroup>().alpha = 0;
        }

        private void Start()
        {
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

            ShowFragments();

        }


        private void ShowFragments()
        {
            LeanTween.alphaCanvas(PanelFrag.GetComponent<CanvasGroup>(), 1, .5f).setOnComplete(CheckFragment);
        }

        private void CheckFragment()
        {
            Debug.Log("total fragments before decreasing = " + total);
            if (total >= CollectableManager.Instance.TotalFragFor1Collectable)
            {
                DecreaseFragment(total % CollectableManager.Instance.TotalFragFor1Collectable);
            }
            else
            {
                PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.TotalFragments, total);
                MoveFragToTop();
            }
        }

        private void DecreaseFragment(int amount)
        {
            Debug.Log("Decreasing to " + amount);
            PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.TotalFragments, amount);
            LeanTween.value(total, amount, 1f).setOnUpdate(OnUpdateFragment).setOnComplete(MoveFragToTop);
        }

        private void MoveFragToTop()
        {
            LeanTween.move(PanelFrag.GetComponent<RectTransform>(), PosFrag.anchoredPosition, .3f).setOnComplete(ShowCollectables);
        }

        private void ShowCollectables()
        {
            Collectable.text = PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.TotalCollectables, 0) + "";
            LeanTween.alphaCanvas(PanelColl.GetComponent<CanvasGroup>(), 1, .5f).setOnComplete(IncreaseCollectable, total / CollectableManager.Instance.TotalFragFor1Collectable);
        }

        private void IncreaseCollectable(object increase)
        {
            if ((int)increase > 0)
            {
                Debug.Log("Increasing collectable by " + increase);
                int tot = PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.TotalCollectables, 0);

                LeanTween.value(tot, tot + (int)increase, 1f).setOnUpdate(OnUpdateCollectable).setOnComplete(MoveCollToTop);
                PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.TotalCollectables, (tot + (int)increase));

            }
            else
                MoveCollToTop();
        }

        private void MoveCollToTop()
        {
            LeanTween.move(PanelColl.GetComponent<RectTransform>(), PosColl.anchoredPosition, .3f);
        }

        private void OnUpdateFragment(float val)
        {
            FragmentCount.text = (int)val + "";
        }

        private void OnUpdateCollectable(float val)
        {
            Collectable.text = (int)val + "";
        }

        public void LoginWithFacebook()
        {
            MyEventManager.Instance.LoginWithFacebook.Dispatch();
        }

        public void LoginWithGoogle()
        {
            MyEventManager.Instance.LoginWithGoogle.Dispatch();
        }


    }
}