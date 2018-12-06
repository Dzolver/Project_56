﻿using Project56;
using UnityEngine;

public class GameData : SingletonMonoBehaviour<GameData>
{
    public enum Direction
    {
        Left = -1, Right = 1
    }

    public GameObject theRunner;

    public Transform theRunnerTransform;

    public Direction direction = Direction.Right; //-1 = left direction, 1= right direction
    public float platformWidth;
    public float CurrentObjectPosX;
    private float PreviousObjectPosX;

    private void OnEnable()
    {
        MyEventManager.Instance.OnGameStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnDisable()
    {
        MyEventManager.Instance.OnGameStateChanged.RemoveListener(OnGameStateChanged);
    }

    private void OnGameStateChanged()
    {
        if (GameStateManager.Instance.CurrentState == GameState.Game)
        {
            CurrentObjectPosX = theRunnerTransform.position.x;
        }
    }

    public float GetNextObjectPosX()
    {
        PreviousObjectPosX = CurrentObjectPosX;
        Debug.Log("Previous : " + PreviousObjectPosX);
        float distance = Random.Range(18f, 25f);
        Debug.Log("Random: " + distance);
        Debug.Log("Runner: " + theRunnerTransform.position.x);
        CurrentObjectPosX = theRunnerTransform.position.x + ((int)direction * distance);
        Debug.Log("CurrentObjectPos: " + CurrentObjectPosX);
        if (Mathf.Abs(PreviousObjectPosX - CurrentObjectPosX) < 7f)
        {
            CurrentObjectPosX += (int)direction * 7f;
            Debug.Log("Current close to previous. New current = " + CurrentObjectPosX);
        }
        return CurrentObjectPosX;
    }
}