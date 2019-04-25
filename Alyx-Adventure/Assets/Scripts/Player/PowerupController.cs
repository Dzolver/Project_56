using System.Collections;
using UnityEngine;

namespace AlyxAdventure
{
    public class PowerupController : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(SpawnPowerups());
        }


        private IEnumerator SpawnPowerups()
        {
            BasePowerup powerup;
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(25, 35));
                if (GameData.Instance.GetCurrentPowerup() == null)
                {
                    int random = Random.Range(1, 4);
                    powerup = ObjectPool.Instance.GetPowerUp((PowerupType)random);
                    MyEventManager.Instance.OnPowerupGenerated.Dispatch(powerup);
                }
            }
        }

    }
}