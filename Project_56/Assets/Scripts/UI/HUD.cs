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
        public TextMeshProUGUI Coins;
        private int CoinsCollected = 0;

        public PowerupTimer powerupTimer;

        private void Start()
        {
            ShowMenu();
            Time.timeScale = 1;
            CoinsCollected = 0;
            if(powerupTimer != null)
            powerupTimer.Deactivate();
        }

        private void OnEnable()
        {
            MyEventManager.Instance.OnScoreUpdated.AddListener(OnScoreUpdated);
            MyEventManager.Instance.OnEnemyKilled.AddListener(OnEnemyKilled);
            MyEventManager.Instance.OnCoinCollected.AddListener(OnCoinCollected);
            MyEventManager.Instance.OnPowerupCollected.AddListener(OnPowerupCollected);
        }

        private void OnDisable()
        {
            if(MyEventManager.Instance!=null)
            {
                MyEventManager.Instance.OnScoreUpdated.RemoveListener(OnScoreUpdated);
                MyEventManager.Instance.OnEnemyKilled.RemoveListener(OnEnemyKilled);
                MyEventManager.Instance.OnCoinCollected.RemoveListener(OnCoinCollected);
                MyEventManager.Instance.OnPowerupCollected.RemoveListener(OnPowerupCollected);



            }
        }

        private void OnPowerupCollected(BasePowerup powerup)
        {
            if (powerupTimer != null)
                powerupTimer.ActivateTimer(powerup.GetPowerupDuration());
        }

        private void OnCoinCollected()
        {
            CoinsCollected++;
            Coins.text = CoinsCollected.ToString(); ;

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
            //Debug.Log("jUMPING");
            MyEventManager.Instance.OnJumpClicked.Dispatch();
        }

        public void OnFallOrSlideClicked()
        {
            //Debug.Log("Falling");
            MyEventManager.Instance.OnFallOrSlideClicked.Dispatch();
        }

        public void OnAttackClicked()
        {
            //MyEventManager.Instance.OnAttackClicked.Dispatch();
        }

        public void Pause()
        {
            HideMenu();
            PauseMenu.ShowMenu();
            Time.timeScale = 0;
        }
    }
}