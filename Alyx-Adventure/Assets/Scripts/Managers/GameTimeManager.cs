using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlyxAdventure
{
    public class GameTimeManager : SingletonMonoBehaviour<GameTimeManager>
    {
        private Coroutine countTime, countMinutes;
        private int TotalSecPlayed;
     
        private float CurrentGameMins;
      
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
                MyEventManager.Instance.OnGameOver.RemoveListener(OnGameOver);
            }
        }

        private void OnGameStarted()
        {
            CurrentGameMins = 0f;
            TotalSecPlayed = PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.TotalSeconds, 0);
            Debug.Log(TotalSecPlayed);
         
            countTime = StartCoroutine(StartCountingTime());
            countMinutes = StartCoroutine(StartCountingMinutes());
        }

        private void OnGameOver()
        {
            StopCoroutine(countTime);
            StopCoroutine(countMinutes);
            PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.TotalSeconds, TotalSecPlayed);

        }

        private IEnumerator StartCountingTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                TotalSecPlayed++;
                MyEventManager.Instance.OnSecondPassed.Dispatch(TotalSecPlayed);
            }
        }

        private IEnumerator StartCountingMinutes()
        {
            while (true)
            {
                yield return new WaitForSeconds(30f);
                CurrentGameMins += 0.5f;
                MyEventManager.Instance.OnMinutesPassed.Dispatch(CurrentGameMins);
            }
        }

    }
}