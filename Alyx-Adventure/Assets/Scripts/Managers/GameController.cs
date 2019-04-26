using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlyxAdventure
{
    public class GameController : MonoBehaviour
    {
        int Multiplier = 1;

        private void OnEnable()
        {
            MyEventManager.Instance.OnPowerupExhausted.AddListener(OnPowerupExhausted);
            MyEventManager.Instance.OnPowerupCollected.AddListener(OnPowerupCollected);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.OnPowerupCollected.RemoveListener(OnPowerupCollected);
                MyEventManager.Instance.OnPowerupExhausted.RemoveListener(OnPowerupExhausted);

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
            }
        }

        private void OnPowerupExhausted(BasePowerup powerup)
        {
            if (powerup.GetPowerupType() == PowerupType.ScoreMultiplier)
            {
                Multiplier = 1;
            }
        }

        private IEnumerator GenerateCoinWave()
        {
            CoinWave coinwave;
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(5, 8));
                coinwave = ObjectPool.Instance.GetCoinWave();
                MyEventManager.Instance.OnCoinWaveGenerated.Dispatch(coinwave);
            }
        }

        private IEnumerator CalculateScore()
        {
            int previousScore;
            while (true)
            {
                yield return new WaitForSeconds(1f);
                previousScore = ScoreManager.Instance.GetScore();
                    ScoreManager.Instance.UpdateScore(Multiplier);
            }
        }
    }
}