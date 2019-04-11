using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public class Platform : MonoBehaviour, IPlatform
    {
        [SerializeField]
        int platformId;

        public Collider2D InvincibilityCollider;

        private void OnEnable()
        {
            MyEventManager.Instance.OnPowerupCollected.AddListener(OnPowerupCollected);
            MyEventManager.Instance.OnPowerupExhausted.AddListener(OnPowerupExhausted);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.OnPowerupCollected.RemoveListener(OnPowerupCollected);
                MyEventManager.Instance.OnPowerupExhausted.RemoveListener(OnPowerupExhausted);
            }


        }

        private void OnPowerupExhausted(BasePowerup powerup)
        {
            if(powerup.GetPowerupType() == PowerupType.Invincibility || powerup.GetPowerupType() == PowerupType.FastRunInvincibility)
            {
                InvincibilityCollider.isTrigger = true;
            }
        }

        private void OnPowerupCollected(BasePowerup powerup)
        {
            if (powerup == null)
                InvincibilityCollider.isTrigger = true;
            else if(powerup.GetPowerupType() == PowerupType.Invincibility || powerup.GetPowerupType() == PowerupType.FastRunInvincibility)
                InvincibilityCollider.isTrigger = false;
        }

        public void ActivateAndSetPosition(Vector3 position)
        {
            gameObject.SetActive(true);
            transform.SetPositionAndRotation(position, Quaternion.identity);
            OnPowerupCollected(null);
        }

        public void ActivateAndSetPosition(Vector3 position, BasePowerup powerup)
        {
            gameObject.SetActive(true);
            transform.SetPositionAndRotation(position, Quaternion.identity);
            OnPowerupCollected(powerup);

        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public int GetPlatformId()
        {
            return platformId;
        }
    }
}