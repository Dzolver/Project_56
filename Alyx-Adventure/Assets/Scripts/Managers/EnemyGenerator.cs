﻿using AlyxAdventure;
using System;
using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    float WaitTime = 7f;
    Coroutine coroutine;

    private void OnEnable()
    {
        MyEventManager.Instance.OnTimePassed.AddListener(OnTimePassed);
    }

    private void OnDisable()
    {
        if (MyEventManager.Instance != null)
        {
            MyEventManager.Instance.OnTimePassed.RemoveListener(OnTimePassed);
        }
    }

    private void Start()
    {
        StartCoroutine(GenerateZombie());
    }

    private void OnTimePassed(float minutes)
    {
        if (minutes < 1f)
        {
            WaitTime = 7f;
        }
        else if (minutes < 2f)
        {
            WaitTime = 6f;
        }
        else if (minutes < 2.5f)
        {
            if (coroutine == null)
                coroutine = StartCoroutine(GenerateRaven());
            WaitTime = 5f;
        }
        else if (minutes <= 3f)
        {
            WaitTime = 4f;
        }
        else
        {
            WaitTime = 3f;
        }
    }

    private IEnumerator GenerateZombie()
    {
        yield return new WaitForSeconds(4f);
        while (true)
        {
            AbstractEnemy zombie = ObjectPool.Instance.GetZombie();
            MyEventManager.Instance.OnEnemyGenerated.Dispatch(zombie);
            yield return new WaitForSeconds(WaitTime);
        }
    }

    private IEnumerator GenerateRaven()
    {
        while (true)
        {
            AbstractEnemy raven = ObjectPool.Instance.GetRaven();
            MyEventManager.Instance.OnEnemyGenerated.Dispatch(raven);
            yield return new WaitForSeconds(WaitTime + UnityEngine.Random.Range(0, 3));
        }
    }
}
