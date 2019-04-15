﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public class GameController : MonoBehaviour
    {
        int Multiplier = 0;
       
        private void OnEnable()
        {
            MyEventManager.Instance.OnPowerupCollected.AddListener(OnPowerupCollected);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.OnPowerupCollected.RemoveListener(OnPowerupCollected);
            }
        }

        private void Start()
        {
            GameData.Instance.theRunner.SetActive(true);
            StartCoroutine(CalculateScore());
            StartCoroutine(GenerateCoinWave());
        }

        private void OnPowerupCollected(BasePowerup powerup)
        {
            if (powerup.GetPowerupType() == PowerupType.ScoreMultiplier)
            {
                Multiplier = ((ScoreMultiplier)powerup).GetMultiplier();
                StartCoroutine(ResetMultiplier(powerup.GetPowerupDuration()));
            }
        }

        private IEnumerator ResetMultiplier(float duration)
        {
            yield return new WaitForSeconds(duration);
            Multiplier = 0;
        }

        private IEnumerator GenerateCoinWave()
        {
            GameObject coinwave;
            while (true)
            {
                yield return new WaitForSeconds(10f);
                int wave = UnityEngine.Random.Range(1, 4);
                coinwave = ObjectPool.Instance.GetCoinWave(wave);
                MyEventManager.Instance.OnCoinWaveGenerated.Dispatch(coinwave.GetComponent<CoinWave>());              
            }
        }

        private IEnumerator CalculateScore()
        {
            int previousScore;
            while (true)
            {
                yield return new WaitForSeconds(1f);
                previousScore = GameData.Instance.CurrentScore;
                if (Multiplier == 0)
                    GameData.Instance.CurrentScore += GameData.Instance.ScorePerSecond;
                else
                    GameData.Instance.CurrentScore += GameData.Instance.ScorePerSecond * Multiplier;
                MyEventManager.Instance.OnScoreUpdated.Dispatch(previousScore, GameData.Instance.CurrentScore);
            }
        }
    }
}