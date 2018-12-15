using Project56;
using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
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
        if (GameStateManager.Instance.CurrentState == GameState.Game)
        {
            StartCoroutine(GenerateEnemy());
        }
        if (GameStateManager.Instance.CurrentState == GameState.Death)
        {
            StopCoroutine(GenerateEnemy());
        }
    }

    private IEnumerator GenerateEnemy()
    {
        int enemyType = 0;
        float x, y;

        while (GameStateManager.Instance.CurrentState == GameState.Game)
        {
            x = GameData.Instance.GetNextObjectPosX();
            if (enemyType == 3 || enemyType == 4)
            {
                IBlock block;
                if (enemyType == 3)
                {
                    block = ObjectPool.Instance.GetJumpBlock().GetComponent<IBlock>();
                    y = -0.6f;
                }
                else
                {
                    block = ObjectPool.Instance.GetSlideBlock().GetComponent<IBlock>();
                    y = 2.1f;
                }
                block.ActivateAndSetPosition(new Vector2(x, y));
            }
            else
            {
                IZombie zombie = ObjectPool.Instance.GetZombie().GetComponent<IZombie>();
                y = -.5f;
                zombie.ActivateAndSetPosition(new Vector2(x, y));
            }

            enemyType = Random.Range(0, 5);
            yield return new WaitForSeconds(5f);
        }
    }
}
