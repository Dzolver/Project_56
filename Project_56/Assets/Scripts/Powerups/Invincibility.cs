using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{


    public class Invincibility : MonoBehaviour,IPowerup
    {
        [SerializeField]
        private PowerupType PowerupType = PowerupType.Invincibility;

        [SerializeField]
        private int Duration;

        public void DeactivatePowerup()
        {
            gameObject.SetActive(false);
        }
        public void ActivatePowerup()
        {
            gameObject.SetActive(true);
        }

        public PowerupType GetPowerupType()
        {
            return PowerupType;
        }

        public void OnPowerupCollected()
        {
            MyEventManager.Instance.OnPowerupCollected.Dispatch(PowerupType);
            DeactivatePowerup();
        }

        public int GetPowerupDuration()
        {
            return Duration;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("MainCamera"))
            {
                DeactivatePowerup();
            }
        }
    }

}