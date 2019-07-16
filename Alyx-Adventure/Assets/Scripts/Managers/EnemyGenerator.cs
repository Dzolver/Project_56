using AlyxAdventure;
using System;
using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    float WaitTime = 7f;
    Coroutine coroutine;

    private void OnEnable()
    {
        MyEventManager.Instance.OnQuarterMinutePassed.AddListener(OnMinutesPassed);
    }

    private void OnDisable()
    {
        if (MyEventManager.Instance != null)
        {
            MyEventManager.Instance.OnQuarterMinutePassed.RemoveListener(OnMinutesPassed);
        }
    }

    private void Start()
    {
        StartCoroutine(GenerateZombie());
    }

    private void OnMinutesPassed(float minutes)
    {
        if (minutes < 1f)
        {
            if (coroutine == null)
                coroutine = StartCoroutine(GenerateRaven());
            WaitTime = 6f;
        }
        else if (minutes < 2f)
        {
            WaitTime = 5f;
        }
        else if (minutes < 2.5f)
        {
            WaitTime = 4f;
        }
        else if (minutes <= 4f)
        {
            WaitTime = 3f;
        }
        else
        {
            WaitTime = 2f;
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
            yield return new WaitForSeconds(WaitTime + UnityEngine.Random.Range(1, 3));
        }
    }
}
