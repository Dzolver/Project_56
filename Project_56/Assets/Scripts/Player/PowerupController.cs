using System.Collections;
using UnityEngine;

namespace Project56
{
    public class PowerupController : MonoBehaviour
    {
        private float InvinicbilityWait = 2f;
        public GameObject InvincibilityGO;

        private void Start()
        {
            StartCoroutine(SpawnPowerups());
        }

       
        private IEnumerator SpawnPowerups()
        {
            yield return new WaitForSeconds(InvinicbilityWait);

            IPowerup powerup;
            powerup = Instantiate(InvincibilityGO, Vector3.right * 30f, Quaternion.identity).GetComponent<IPowerup>();


            //Activation logic
            powerup.ActivatePowerup();
        }

    }
}