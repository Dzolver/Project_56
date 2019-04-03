using System;
using TMPro;
using UnityEngine;

namespace Project56
{
    public class HUD : Menu
    {
        public PlayerController playerCtrl;
        public Menu PauseMenu;

        public TextMeshProUGUI Score;
        public TextMeshProUGUI Kills;

        private void Start()
        {
            ShowMenu();
            Time.timeScale = 1;
        }

        private void OnEnable()
        {
            MyEventManager.Instance.OnScoreUpdated.AddListener(OnScoreUpdated);
            MyEventManager.Instance.OnEnemyKilled.AddListener(OnEnemyKilled);
        }

        private void OnDisable()
        {
            if(MyEventManager.Instance!=null)
            {
                MyEventManager.Instance.OnScoreUpdated.AddListener(OnScoreUpdated);
                MyEventManager.Instance.OnEnemyKilled.RemoveListener(OnEnemyKilled);

            }
        }

        private void OnEnemyKilled(int UpdatedValue)
        {
            Kills.text = "" + UpdatedValue;
        }

        private void OnScoreUpdated(int PreviousScore, int UpdatedScore)
        {
            LeanTween.value(Score.gameObject,OnScoreUpdated, PreviousScore, UpdatedScore, 0.5f);
        }

        private void OnScoreUpdated(float updatedScore)
        {
            Score.text = "" + (int)updatedScore;
        }

        public void OnJumpClicked()
        {
            MyEventManager.Instance.OnJumpClicked.Dispatch();
        }

        public void OnFallOrSlideClicked()
        {
            Debug.Log("Falling");
            MyEventManager.Instance.OnFallOrSlideClicked.Dispatch();
        }

        public void OnAttackClicked()
        {
            MyEventManager.Instance.OnAttackClicked.Dispatch();
        }

        public void Pause()
        {
            HideMenu();
            PauseMenu.ShowMenu();
            Time.timeScale = 0;
        }
    }
}