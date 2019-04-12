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
        //float x, y;
        yield return new WaitForSeconds(2f);
        while (true)
        {
            //x = GameData.Instance.GetNextObjectPosX();  y = -4f;
            IZombie zombie = ObjectPool.Instance.GetZombie().GetComponent<IZombie>();
            MyEventManager.Instance.OnEnemyGenerated.Dispatch(zombie);
            //zombie.ActivateAndSetPosition(new Vector2(x, y));
            yield return new WaitForSeconds(5f);
        }
    }
}
