using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace AlyxAdventure
{
    public class GameController : MonoBehaviour
    {
        private void Awake()
        {
            ObjectPool.Instance.ShufflePlatforms();
        }

        private void OnEnable()
        {
            MyEventManager.Instance.OnGameOver.AddListener(OnGameOver);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.OnGameOver.RemoveListener(OnGameOver);
            }
        }

        private void Start()
        {
            MyEventManager.Instance.OnGameStarted.Dispatch();
            StartCoroutine(GenerateCoinWave());
        }

        public void OnGameOver()
        {
            SceneManager.LoadScene(3);
        }

      
        private IEnumerator GenerateCoinWave()
        {
            CoinWave coinwave;
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(3, 5));
                coinwave = ObjectPool.Instance.GetCoinWave();
                MyEventManager.Instance.OnCoinWaveGenerated.Dispatch(coinwave);
            }
        }
    }
}