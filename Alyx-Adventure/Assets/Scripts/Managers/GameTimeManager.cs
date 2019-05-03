using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlyxAdventure
{
    public class GameTimeManager : SingletonMonoBehaviour<GameTimeManager>
    {
        Coroutine coroutine;
        int TotalSecPlayed;

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
            Debug.Log("Start Total played = " + TotalSecPlayed);
            coroutine = StartCoroutine(StartCountingTime());
        }

        private IEnumerator StartCountingTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                TotalSecPlayed++;
                if (TotalSecPlayed % 300 == 0)
                    MyEventManager.Instance.GenerateFragment.Dispatch();
            }
        }


        private void OnGameOver()
        {
            StopCoroutine(coroutine);
            PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.TotalSeconds, TotalSecPlayed);
        }

    }
}