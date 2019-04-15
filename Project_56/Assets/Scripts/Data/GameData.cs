using System;
using System.Collections;
using Project56;
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
    
    public int ScorePerSecond;
    public float MultiplierPerKill;
    public int CurrentScore = 0;
    public int TotalKills = 0;

    private BasePowerup currentPowerup = null;


    private void Start()
    {
        StartCoroutine(IncreaseSpeed());
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

    public BasePowerup GetCurrentPoweruup()
    {
        return currentPowerup;
    }

    public void AddKills()
    {
        TotalKills += 1;
        MyEventManager.Instance.OnEnemyKilled.Dispatch(TotalKills);
    }
}

