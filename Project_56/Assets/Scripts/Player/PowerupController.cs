using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public class PowerupController : MonoBehaviour
    {
        private float InvinicbilityWait = 2f;
        public GameObject InvincibilityGO;

        private void Start()
        {

        }

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
                StartCoroutine(SpawnPowerups());
            }
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