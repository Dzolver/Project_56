﻿using Project56;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform startPoint;

    [SerializeField]
    private Platform CurrentPlatform, LeftPlatform, RightPlatform;

    private void OnEnable()
    {
        MyEventManager.Instance.OnGameStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnDisable()
    {
        if (MyEventManager.Instance != null)
        {
            MyEventManager.Instance.OnGameStateChanged.RemoveListener(OnGameStateChanged);
        }
    }

    private void OnGameStateChanged()
    {
        switch (GameStateManager.Instance.CurrentState)
        {
            case GameState.Game:

                {
                    if (CurrentPlatform == null)
                    {
                        CurrentPlatform = ObjectPool.Instance.GetPlatform().GetComponent<Platform>();
                        GameData.Instance.platformWidth = CurrentPlatform.GetComponentInChildren<BoxCollider2D>().size.x;
                        CurrentPlatform.ActivateAndSetPosition(startPoint.localPosition);
                    }
                    else
                        CurrentPlatform.SetActive(true);
                    if (LeftPlatform == null)
                    {
                        LeftPlatform = ObjectPool.Instance.GetPlatform().GetComponent<Platform>();
                        LeftPlatform.ActivateAndSetPosition(new Vector2(startPoint.position.x - GameData.Instance.platformWidth, startPoint.position.y));
                    }
                    else
                        LeftPlatform.SetActive(true);
                    if (RightPlatform == null)
                    {
                        RightPlatform = ObjectPool.Instance.GetPlatform().GetComponent<Platform>();
                        RightPlatform.ActivateAndSetPosition(new Vector2(startPoint.position.x + GameData.Instance.platformWidth, startPoint.position.y));
                    }
                    else
                        RightPlatform.SetActive(true);

                    break;
                }
        }
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if (GameStateManager.Instance.CurrentState == GameState.Game)
        {
            if (GameData.Instance.theRunnerTransform.position.x - CurrentPlatform.transform.position.x > GameData.Instance.platformWidth / 2)
                ActivateRightPlatform();
            else if (CurrentPlatform.transform.position.x - GameData.Instance.theRunnerTransform.position.x > GameData.Instance.platformWidth / 2)
                ActivateLeftPlatform();
        }
    }

    private void ActivateRightPlatform()
    {
        Platform platform = ObjectPool.Instance.GetPlatform().GetComponent<Platform>();
        platform.ActivateAndSetPosition(new Vector2(RightPlatform.transform.position.x + GameData.Instance.platformWidth, RightPlatform.transform.position.y));
        LeftPlatform.GetComponent<Platform>().Deactivate();
        LeftPlatform = CurrentPlatform;
        CurrentPlatform = RightPlatform;
        RightPlatform = platform;
    }

    private void ActivateLeftPlatform()
    {
        Platform platform = ObjectPool.Instance.GetPlatform().GetComponent<Platform>();
        platform.ActivateAndSetPosition(new Vector2(LeftPlatform.transform.position.x - GameData.Instance.platformWidth, LeftPlatform.transform.position.y));
        RightPlatform.GetComponent<Platform>().Deactivate();
        RightPlatform = CurrentPlatform;
        CurrentPlatform = LeftPlatform;
        LeftPlatform = platform;
    }
}