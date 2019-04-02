﻿using System.Collections;
using UnityEngine;

namespace Project56
{
    public class GameController : MonoBehaviour
    {

        private void Start()
        {
            GameData.Instance.theRunner.SetActive(true);
            StartCoroutine(GenerateCoinWave());
        }

        private IEnumerator GenerateCoinWave()
        {
            GameObject coinwave;
            while (true)
            {
                yield return new WaitForSeconds(9f);
                int wave = UnityEngine.Random.Range(1, 4);
                coinwave = ObjectPool.Instance.GetCoinWave(wave);
                Vector2 pos = new Vector2(GameData.Instance.GetNextObjectPosX(), 2f);
                GameData.Instance.CurrentObjectPosX = pos.x + ((int)GameData.Instance.direction * coinwave.GetComponent<CoinWave>().length);
                coinwave.GetComponent<CoinWave>().ActivateAndSetPosition(pos);
            }
        }
    }
}