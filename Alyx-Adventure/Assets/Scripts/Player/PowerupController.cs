using System.Collections;
using UnityEngine;

namespace AlyxAdventure
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
            BasePowerup powerup;
            while (true)
            {
                yield return new WaitForSeconds(WaitTime);
                int random = Random.Range(1, 4);
                powerup = ObjectPool.Instance.GetPowerUp((PowerupType)random);
                MyEventManager.Instance.OnPowerupGenerated.Dispatch(powerup);
            }

        }

    }
}