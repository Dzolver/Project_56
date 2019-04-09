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
        float x, y;
        while (true)
        {
            x = GameData.Instance.GetNextObjectPosX();
            IZombie zombie = ObjectPool.Instance.GetZombie().GetComponent<IZombie>();
            y = -4f;
            zombie.ActivateAndSetPosition(new Vector2(x, y));
            yield return new WaitForSeconds(5f);
        }
    }
}
