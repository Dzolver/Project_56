using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlyxAdventure
{
    public class GameController : MonoBehaviour
    {
        int time, effectTime;

        public bool quickWeather = false;

        public ParticleSystem SnowEffect, RainEffect;
        public FogEffect fogEffect;

        private void Awake()
        {
            time = 0;
            ObjectPool.Instance.ShufflePlatforms();
        }

        private void OnEnable()
        {
            MyEventManager.Instance.OnGameOver.AddListener(OnGameOver);
            MyEventManager.Instance.OnSecondPassed.AddListener(OnSecondPassed);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.OnGameOver.RemoveListener(OnGameOver);
                MyEventManager.Instance.OnSecondPassed.RemoveListener(OnSecondPassed);

            }
        }

        private void Start()
        {
            MyEventManager.Instance.OnGameStarted.Dispatch();
            StartCoroutine(GenerateCoinWave());
            if (!quickWeather)
            {
                effectTime = Random.Range(15, 30);
            }
            else
            {
                effectTime = 12;
            }
            Debug.Log("effect time = " + effectTime);
        }

        public void OnGameOver()
        {
            SceneManager.LoadScene(3);
        }

        private void OnSecondPassed(int obj)
        {
            time++;
            if (time == effectTime)
            {
                int effect = Random.Range(0, 3);
                Debug.Log("effect = " + effect);
                switch (effect)
                {
                    case 0:
                        SnowEffect.Play();
                        break;
                    case 1:
                        RainEffect.Play();
                        break;
                    case 2:
                        fogEffect.Activate();
                        break;
                }
                if (!quickWeather)
                {
                    effectTime = Random.Range(effectTime + 15, effectTime + 30);
                }
                else
                {
                    effectTime += 12;
                }

            }

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