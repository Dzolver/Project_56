using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public abstract class BasePowerup : MonoBehaviour
    {
        [SerializeField]
        protected PowerupType powerType;

        public abstract int GetPowerupDuration();
       
        public PowerupType GetPowerupType()
        {
            return powerType;
        }

        public void OnPowerupCollected()
        {
            MyEventManager.Instance.OnPowerupCollected.Dispatch(this);
            DeactivatePowerup();
        }

        public void DeactivatePowerup()
        {
            gameObject.SetActive(false);
        }
        public void ActivateAndSetPosition(Vector2 pos)
        {
            gameObject.SetActive(true);
            gameObject.transform.SetPositionAndRotation(pos, Quaternion.identity);
        }
    }

    public enum PowerupType
    {
        None,
        Invincibility,
        ScoreMultiplier,
        FastRunInvincibility
    }



}