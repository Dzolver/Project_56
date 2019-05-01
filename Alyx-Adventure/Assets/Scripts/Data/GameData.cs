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

    private int FragmentsCollected;

    private void Start()
    {
        ScoreManager.Instance.ResetScore();
        FragmentsCollected = PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.FragmentCount, 0);
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
        MyEventManager.Instance.OnFragmentCollected.AddListener(OnFragmentCollected);

    }

    private void OnDisable()
    {
        if (MyEventManager.Instance != null)
        {
            MyEventManager.Instance.ChangeMoveDirection.RemoveListener(ChangeMoveDirection);
            MyEventManager.Instance.OnPowerupCollected.RemoveListener(OnPowerupCollected);
            MyEventManager.Instance.OnPowerupExhausted.RemoveListener(OnPowerupExhausted);
            MyEventManager.Instance.OnFragmentCollected.RemoveListener(OnFragmentCollected);

        }
    }

    private void OnFragmentCollected(CollectableFragmentBase fragment)
    {
        FragmentsCollected++;
        PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.FragmentCount, FragmentsCollected);
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

    public int GetFragmentCount()
    {
        return FragmentsCollected;
    }

    public void AddKills()
    {
        TotalKills += 1;
        MyEventManager.Instance.OnEnemyKilled.Dispatch(TotalKills);
    }

}

