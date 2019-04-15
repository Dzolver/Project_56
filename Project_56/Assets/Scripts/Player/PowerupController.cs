using System.Collections;
using UnityEngine;

namespace Project56
{
    public class PowerupController : MonoBehaviour
    {
        private float WaitTime = 25f;

        private void Start()
        {
            StartCoroutine(SpawnPowerups());
        }


        private IEnumerator SpawnPowerups()
        {
            GameObject go;
            while (true)
            {
                yield return new WaitForSeconds(WaitTime);
                int random = Random.Range(1, 4);
                go = ObjectPool.Instance.GetPowerUp((PowerupType)random);
                MyEventManager.Instance.OnPowerupGenerated.Dispatch(go.GetComponent<BasePowerup>());
            }

        }

    }
}