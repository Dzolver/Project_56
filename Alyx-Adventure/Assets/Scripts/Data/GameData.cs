using System;
using System.Collections;
using AlyxAdventure;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Direction
{
    Left = -1, Right = 1
}

public class GameData : SingletonMonoBehaviour<GameData>
{

    public GameObject theRunner;
    public Vector3 RunnerVelocity;

    public Transform theRunnerTransform;

    public Direction direction = Direction.Right; //-1 = left direction, 1= right direction

    public float MultiplierPerKill;
    public int TotalKills = 0;
    public float MinutesSinceGame = 0f;

    private BasePowerup currentPowerup = null;


    private void Start()
    {
        StartCoroutine(IncreaseTime());
        StartCoroutine(IncreaseSpeed());
    }

    private IEnumerator IncreaseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);
            MinutesSinceGame += 0.5f;
            MyEventManager.Instance.OnTimePassed.Dispatch(MinutesSinceGame);
        }
    }

    private IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            MyEventManager.Instance.IncreaseSpeed.Dispatch();
        }

    }

    private void OnEnable()
    {
        MyEventManager.Instance.ChangeMoveDirection.AddListener(ChangeMoveDirection);
        MyEventManager.Instance.OnPowerupCollected.AddListener(OnPowerupCollected);
        MyEventManager.Instance.OnPowerupExhausted.AddListener(OnPowerupExhausted);


    }

    private void OnDisable()
    {
        if (MyEventManager.Instance != null)
        {
            MyEventManager.Instance.ChangeMoveDirection.RemoveListener(ChangeMoveDirection);
            MyEventManager.Instance.OnPowerupCollected.RemoveListener(OnPowerupCollected);
            MyEventManager.Instance.OnPowerupExhausted.RemoveListener(OnPowerupExhausted);


        }
    }

    private void OnPowerupExhausted(BasePowerup powerup)
    {
        currentPowerup = null;
    }

    private void OnPowerupCollected(BasePowerup powerup)
    {
        currentPowerup = powerup;
    }

    private void ChangeMoveDirection(Direction direction)
    {
        this.direction = direction;
    }

    public BasePowerup GetCurrentPowerup()
    {
        return currentPowerup;
    }

    public void AddKills()
    {
        TotalKills += 1;
        MyEventManager.Instance.OnEnemyKilled.Dispatch(TotalKills);
    }

}

