using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlyxAdventure
{
    public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
    {
        private int ScoreMultiplier = 1;
        private int GameScore;

        public int ScorePerSecond;

        private void OnEnable()
        {
            MyEventManager.Instance.OnPowerupExhausted.AddListener(OnPowerupExhausted);
            MyEventManager.Instance.OnPowerupCollected.AddListener(OnPowerupCollected);
            MyEventManager.Instance.OnGameStarted.AddListener(ResetScore);
            MyEventManager.Instance.OnSecondPassed.AddListener(UpdateScore);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.OnPowerupCollected.RemoveListener(OnPowerupCollected);
                MyEventManager.Instance.OnPowerupExhausted.RemoveListener(OnPowerupExhausted);
                MyEventManager.Instance.OnGameStarted.RemoveListener(ResetScore);
                MyEventManager.Instance.OnSecondPassed.RemoveListener(UpdateScore);

            }
        }

        private void OnPowerupCollected(BasePowerup powerup)
        {
            if (powerup.GetPowerupType() == PowerupType.ScoreMultiplier)
            {
                ScoreMultiplier = ((ScoreMultiplier)powerup).GetMultiplier();
            }
        }

        private void OnPowerupExhausted(BasePowerup powerup)
        {
            if (powerup.GetPowerupType() == PowerupType.ScoreMultiplier)
            {
                ScoreMultiplier = 1;
            }
        }

        public void UpdateScore(int totalSecPlayed)
        {
            int previousScore = GameScore;
            GameScore += ScorePerSecond * ScoreMultiplier;
            MyEventManager.Instance.OnScoreUpdated.Dispatch(previousScore, GameScore);
        }

        public int GetScore()
        {
            return GameScore;
        }

        public void ResetScore()
        {
            GameScore = 0;
            ScoreMultiplier = 1;
        }
    }

}