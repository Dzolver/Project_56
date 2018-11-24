﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public class DeathMenu : Menu
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
            if (GameStateManager.Instance.CurrentState == GameState.Death)
                ShowMenu();
        }

        public void PlayAgain()
        {
            GameStateManager.Instance.UpdateState(GameState.Game);
            HideMenu();
        }

        public void ReturnToMain()
        {
            HideMenu();
            GameStateManager.Instance.UpdateState(GameState.MainMenu);
        }
    }
}