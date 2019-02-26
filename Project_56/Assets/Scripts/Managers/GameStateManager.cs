using System.Collections;
using UnityEngine;

namespace Project56
{
    public class GameStateManager : SingletonMonoBehaviour<GameStateManager>
    {
        [SerializeField]
        private GameState m_CurrentState;

        private void OnEnable()
        {
            MyEventManager.Instance.UpdateState.AddListener(UpdateState);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.UpdateState.RemoveListener(UpdateState);
            }

        }

        private void UpdateState(GameState gameState)
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
        Splash,
        MainMenu,
        Game,
        Death,
        Paused,
    }
}