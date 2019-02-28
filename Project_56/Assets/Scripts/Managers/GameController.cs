using System.Collections;
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
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.OnGameStateChanged.RemoveListener(OnGameStateChanged);
            }
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
            GameObject coinwave;
            while (GameStateManager.Instance.CurrentState == GameState.Game)
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