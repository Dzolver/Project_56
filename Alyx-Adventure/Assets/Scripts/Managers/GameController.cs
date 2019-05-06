using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace AlyxAdventure
{
    public class GameController : MonoBehaviour
    {
      
        private void OnEnable()
        {
            MyEventManager.Instance.OnGameOver.AddListener(OnGameOver);
            MyEventManager.Instance.GenerateFragment.AddListener(GenerateFragment);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.GenerateFragment.RemoveListener(GenerateFragment);
                MyEventManager.Instance.OnGameOver.AddListener(OnGameOver);

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

        private void GenerateFragment()
        {
            CollectableFragmentBase fragment = ObjectPool.Instance.GetFragment();
            fragment.ActivateAndSetPosition(new Vector2(GameData.Instance.theRunnerTransform.position.x +
                           (int)GameData.Instance.direction * 16f, GameData.Instance.theRunnerTransform.position.y + 1f));
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
    }
}