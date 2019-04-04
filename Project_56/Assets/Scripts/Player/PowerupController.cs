using System.Collections;
using UnityEngine;

namespace Project56
{
    public class PowerupController : MonoBehaviour
    {
        private float InvinicbilityWait = 2f;
        public GameObject InvincibilityGO;
        public GameObject ScoreMultiplier;
        public GameObject FastRun;

        private void Start()
        {
            StartCoroutine(SpawnPowerups());
        }

       
        private IEnumerator SpawnPowerups()
        {
            yield return new WaitForSeconds(InvinicbilityWait);
            BasePowerup powerup;
            powerup = Instantiate(InvincibilityGO, Vector3.right * 30f, Quaternion.identity).GetComponent<BasePowerup>();
            //Activation logic
            powerup.ActivatePowerup();

            powerup = Instantiate(FastRun, new Vector3(40f,-2.5f,0), Quaternion.identity).GetComponent<BasePowerup>();
            powerup.ActivatePowerup();
        }

    }
}