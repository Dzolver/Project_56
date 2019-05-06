using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlyxAdventure
{
    public class GameTimeManager : SingletonMonoBehaviour<GameTimeManager>
    {
        private Coroutine countTime, countMinutes;
        private int TotalSecPlayed;
        private int FragSpawnTime;

        public float MinutesSinceGame;
      

        private void OnEnable()
        {
            MyEventManager.Instance.OnGameStarted.AddListener(OnGameStarted);
            MyEventManager.Instance.OnGameOver.AddListener(OnGameOver);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.OnGameStarted.RemoveListener(OnGameStarted);
                MyEventManager.Instance.OnGameOver.AddListener(OnGameOver);
            }
        }

        private void OnGameStarted()
        {
            TotalSecPlayed = PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.TotalSeconds, 0);
            Debug.Log(TotalSecPlayed);
            MinutesSinceGame = 0f;
            FragSpawnTime = TotalSecPlayed + Random.Range(40, 70);
            countTime = StartCoroutine(StartCountingTime());
            countMinutes = StartCoroutine(StartCountingMinutes());
        }

        private IEnumerator StartCountingTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                TotalSecPlayed++;
                MyEventManager.Instance.OnSecondPassed.Dispatch();
                if (TotalSecPlayed == FragSpawnTime)
                    MyEventManager.Instance.GenerateFragment.Dispatch();
            }
        }


        private IEnumerator StartCountingMinutes()
        {
            while (true)
            {
                yield return new WaitForSeconds(30f);
                MinutesSinceGame += 0.5f;
                MyEventManager.Instance.OnMinutesPassed.Dispatch(MinutesSinceGame);
            }
        }

        private void OnGameOver()
        {
            StopCoroutine(countTime);
            StopCoroutine(countMinutes);
            PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.TotalSeconds, TotalSecPlayed);

        }

    }
}