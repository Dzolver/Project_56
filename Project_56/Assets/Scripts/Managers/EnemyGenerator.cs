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
        yield return new WaitForSeconds(2f);
        while (true)
        {
            IZombie zombie = ObjectPool.Instance.GetZombie().GetComponent<IZombie>();
            MyEventManager.Instance.OnEnemyGenerated.Dispatch(zombie);
            yield return new WaitForSeconds(5f);
        }
    }
}
