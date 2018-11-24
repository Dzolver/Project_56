using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public class HUD : Menu
    {
        public void OnJumpClicked()
        {
            Debug.Log("Jumping");
            MyEventManager.Instance.OnJumpClicked.Dispatch();
        }
        public void OnFallClicked() {
            Debug.Log("Falling");
            MyEventManager.Instance.OnFallClicked.Dispatch();
        }

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
                ShowMenu();
                Time.timeScale = 1;
            }
            else
                HideMenu();
        }

        public void Pause()
        {
            Time.timeScale = 0;
            GameStateManager.Instance.UpdateState(GameState.Paused);
        }
    }
}