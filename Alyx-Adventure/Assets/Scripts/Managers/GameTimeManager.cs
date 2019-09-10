using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlyxAdventure
{
    public class GameTimeManager : SingletonMonoBehaviour<GameTimeManager>
    {
        private Coroutine countTime, countMinutes;
        private float CurrentGameMins;
        private int TotalSecPlayed;

        public bool CanDie;

        private void OnEnable()
        {
            MyEventManager.Instance.OnGameStarted.AddListener(OnGameStarted);
            MyEventManager.Instance.OnGameOver.AddListener(OnGameOver);
            MyEventManager.Instance.EnableDeath.AddListener(EnableDeath);

        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.OnGameStarted.RemoveListener(OnGameStarted);
                MyEventManager.Instance.OnGameOver.RemoveListener(OnGameOver);
                MyEventManager.Instance.EnableDeath.RemoveListener(EnableDeath);

            }
        }

        private void Start()
        {
            CanDie = true;
        }

        private void EnableDeath(bool enable)
        {
            CanDie = enable;
        }

        private void OnGameStarted()
        {
            CurrentGameMins = 0f;
            TotalSecPlayed = PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.TotalSeconds, 0);

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
                yield return new WaitForSeconds(15f);
                CurrentGameMins += 0.25f;
                MyEventManager.Instance.OnQuarterMinutePassed.Dispatch(CurrentGameMins);
            }
        }

    }
}