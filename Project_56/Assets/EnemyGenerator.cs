using Project56;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public Transform Runner;

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
        Vector2 currentEnemyPos, previousEnemyPos = Vector2.zero;
        int enemyType = 0;
        while (GameStateManager.Instance.CurrentState == GameState.Game)
        {
            float distance = Random.Range(20f, 25f);
            if (enemyType == 3 || enemyType == 4)
            {
                IBlock block;
                if (enemyType == 3)
                {
                    block = ObjectPool.Instance.GetJumpBlock().GetComponent<IBlock>();
                    currentEnemyPos = new Vector2(Runner.position.x + (Camera.main.GetComponent<CameraController>().direction * distance), -0.6f);
                }
                else
                {
                    block = ObjectPool.Instance.GetSlideBlock().GetComponent<IBlock>();
                    currentEnemyPos = new Vector2(Runner.position.x + (Camera.main.GetComponent<CameraController>().direction * distance), .2f);
                }
                if (Mathf.Abs(previousEnemyPos.x - currentEnemyPos.x) < 10f)
                    currentEnemyPos.x += Camera.main.GetComponent<CameraController>().direction * 10f;
                block.ActivateAndSetPosition(currentEnemyPos);
            }
            else
            {
                IZombie zombie = ObjectPool.Instance.GetZombie().GetComponent<IZombie>();
                currentEnemyPos = new Vector2(Runner.position.x + (Camera.main.GetComponent<CameraController>().direction * distance), -.5f);
                if (Mathf.Abs(previousEnemyPos.x - currentEnemyPos.x) < 10f)
                    currentEnemyPos.x += Camera.main.GetComponent<CameraController>().direction * 10f;
                zombie.ActivateAndSetPosition(currentEnemyPos);
            }

            previousEnemyPos = currentEnemyPos;
            enemyType = Random.Range(0, 5);
            yield return new WaitForSeconds(5f);
        }
    }
}
