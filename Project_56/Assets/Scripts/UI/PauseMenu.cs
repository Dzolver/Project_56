using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public class PauseMenu : Menu
    {
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
            if (GameStateManager.Instance.CurrentState == GameState.Paused)
                ShowMenu();
        }

        public void Resume()
        {
            HideMenu();
            GameStateManager.Instance.UpdateState(GameState.Game);
        }

        public void ReturnToMain()
        {
            HideMenu();
            GameStateManager.Instance.UpdateState(GameState.MainMenu);
        }
    }
}