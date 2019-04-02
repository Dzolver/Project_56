using Project56;
using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(GenerateEnemy());
    }


    private IEnumerator GenerateEnemy()
    {
        int enemyType = 0;
        float x, y;

        while (true)
        {
            x = GameData.Instance.GetNextObjectPosX();
            if (enemyType == 3 || enemyType == 4)
            {
                IBlock block;
                if (enemyType == 3)
                {
                    block = ObjectPool.Instance.GetJumpBlock().GetComponent<IBlock>();
                    y = -4.2f;
                }
                else
                {
                    block = ObjectPool.Instance.GetSlideBlock().GetComponent<IBlock>();
                    y = -1f;
                }
                block.ActivateAndSetPosition(new Vector2(x, y));
            }
            else
            {
                IZombie zombie = ObjectPool.Instance.GetZombie().GetComponent<IZombie>();
                y = -4f;
                zombie.ActivateAndSetPosition(new Vector2(x, y));
            }

            enemyType = Random.Range(0, 5);
            yield return new WaitForSeconds(5f);
        }
    }
}
