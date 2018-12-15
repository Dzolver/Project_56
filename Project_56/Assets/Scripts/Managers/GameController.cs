using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public class GameController : MonoBehaviour
    {
        private void OnEnable()
        {
            MyEventManager.Instance.OnGameStateChanged.AddListener(OnGameStateChanged);
        }

        private void OnDisable()
        {
            MyEventManager.Instance.OnGameStateChanged.RemoveListener(OnGameStateChanged);
        }

        private void OnGameStateChanged()
        {
            if (GameStateManager.Instance.CurrentState == GameState.Game)
            {
                GameData.Instance.theRunner.SetActive(true);
                StartCoroutine(GenerateCoinWave());
            }
            if (GameStateManager.Instance.CurrentState == GameState.Death)
            {
                GameData.Instance.theRunner.SetActive(false);
                StopCoroutine(GenerateCoinWave());
            }
        }

        private IEnumerator GenerateCoinWave()
        {
            while (GameStateManager.Instance.CurrentState == GameState.Game)
            {
                yield return new WaitForSeconds(9f);
                int coins = UnityEngine.Random.Range(5, 11);
                Vector2 pos = new Vector2(GameData.Instance.GetNextObjectPosX(), .05f);
                GameData.Instance.CurrentObjectPosX = pos.x + ((int)GameData.Instance.direction * coins);
                for (int i = 0; i < coins; i++)
                {
                    GameObject coin = ObjectPool.Instance.GetCoin();
                    coin.GetComponent<Coin>().ActivateAndSetPosition(new Vector2(pos.x + ((int)GameData.Instance.direction * i), pos.y));
                }
            }
        }
    }
}