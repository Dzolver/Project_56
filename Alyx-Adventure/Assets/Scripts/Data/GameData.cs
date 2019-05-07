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

    private BasePowerup currentPowerup = null;

    private int TotalFragments;
    

    private void Start()
    {
        TotalFragments = PrefManager.Instance.GetIntPref(PrefManager.PreferenceKey.TotalFragments, 0);
    }

    private void OnEnable()
    {
        MyEventManager.Instance.OnGameStarted.AddListener(OnGameStarted);
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
            MyEventManager.Instance.OnGameStarted.RemoveListener(OnGameStarted);
        }
    }

    private void OnGameStarted()
    {
        theRunner.SetActive(true);
    }

    private void OnFragmentCollected(CollectableFragmentBase fragment)
    {
        TotalFragments++;
        PrefManager.Instance.UpdateIntPref(PrefManager.PreferenceKey.TotalFragments, TotalFragments);
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
        return TotalFragments;
    }

    public void AddKills()
    {
        TotalKills += 1;
        MyEventManager.Instance.OnEnemyKilled.Dispatch(TotalKills);
    }

}

