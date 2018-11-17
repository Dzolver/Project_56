using System;
using System.Collections;
using UnityEngine;

namespace Project56
{
    public class GameStateManager : SingletonMonoBehaviour<GameStateManager>
    {
        [SerializeField]
        private GameState m_CurrentState;

        public void UpdateState(GameState gameState)
        {
            StartCoroutine(UpdState(gameState));
        }

        private IEnumerator UpdState(GameState gameState)
        {
            yield return new WaitForEndOfFrame();
            m_CurrentState = gameState;
            MyEventManager.Instance.OnGameStateChanged.Dispatch();
        }

        public GameState CurrentState
        {
            get
            {
                return m_CurrentState;
            }
        }
    }

    public enum GameState
    {
        MainMenu,
        Game,
        Death,
        Paused,
    }
}