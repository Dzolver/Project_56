﻿using Project56;
using System;
using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    float WaitTime = 7f;

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
        StartCoroutine(GenerateEnemy());
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
            WaitTime = 5f;
        }
        else if (minutes < 3f)
        {
            WaitTime = 4f;
        }
        else
        {
            WaitTime = 3f;
        }
    }

    private IEnumerator GenerateEnemy()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            IZombie zombie = ObjectPool.Instance.GetZombie().GetComponent<IZombie>();
            MyEventManager.Instance.OnEnemyGenerated.Dispatch(zombie);
            yield return new WaitForSeconds(WaitTime);
        }
    }
}
