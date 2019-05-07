using AlyxAdventure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : SingletonMonoBehaviour<CollectableManager>
{
    private int FragSpawnTime;

    [SerializeField]
    private int TotalSecToEarnOne = 900;

    private void OnEnable()
    {
        MyEventManager.Instance.OnGameStarted.AddListener(OnGameStarted);
        MyEventManager.Instance.OnSecondPassed.AddListener(OnSecondPassed);
    }

    private void OnDisable()
    {
        if (MyEventManager.Instance != null)
        {
            MyEventManager.Instance.OnGameStarted.AddListener(OnGameStarted);
            MyEventManager.Instance.OnSecondPassed.AddListener(OnSecondPassed);
        }
    }

    private void OnGameStarted()
    {
        FragSpawnTime = PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.TotalSeconds, 0) + 60;
    }

    private void OnSecondPassed(int TotalSecPlayed)
    {
        if (TotalSecPlayed == FragSpawnTime)
            GenerateFragment();
        if (TotalSecPlayed > 0 && TotalSecPlayed % TotalSecToEarnOne == 0)
        {
            PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.FragmentFromTime,
                                               PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.FragmentFromTime, 0) + 1);
        }
    }

    private void GenerateFragment()
    {
        CollectableFragmentBase fragment = ObjectPool.Instance.GetFragment();
        fragment.ActivateAndSetPosition(new Vector2(GameData.Instance.theRunnerTransform.position.x +
                       (int)GameData.Instance.direction * 16f, GameData.Instance.theRunnerTransform.position.y + 1f));
    }


}
