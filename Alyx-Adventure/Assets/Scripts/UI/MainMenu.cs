using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using System;

namespace AlyxAdventure
{
    public class MainMenu : Menu
    {
        public TextMeshProUGUI FragmentCount;

        private void Start()
        {
            int total = PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.TotalFragments, 0);
            int earned = PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.FragmentFromTime, 0);

            if (earned > 0)
            {
                ShowFragmentText(total + earned);
                PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.FragmentFromTime, 0);
                PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.TotalFragments, earned + total);
            }
            else
                ShowFragmentText(total);

        }

        void ShowFragmentText(int count)
        {
            LeanTween.value(0f, count, .5f).setOnUpdate(OnUpdate);
        }

        private void OnUpdate(float val)
        {
            FragmentCount.text = (int)val + "";
        }

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
    }
}