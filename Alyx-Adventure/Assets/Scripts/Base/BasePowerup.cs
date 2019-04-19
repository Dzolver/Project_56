using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlyxAdventure
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

        public void ActivateAndSetPosition(Vector2 pos, Transform parent)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = pos;
            gameObject.transform.SetParent(parent);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(GameStrings.MainCamera))
            {
                DeactivatePowerup();
            }
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