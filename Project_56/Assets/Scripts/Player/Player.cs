using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    [RequireComponent(typeof(PlayerController))]
    public class Player : MonoBehaviour, IPlayer
    {
        public bool attacked;
        private bool IsInvincible = false;

        public void ActivateAndSetPosition(Vector3 vector3)
        {
            throw new NotImplementedException();
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
            
        }

        public void Deactivate()
        {
            LeanTween.color(gameObject, Color.white, 0.1f);
            gameObject.SetActive(false);
            gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public void OnDie()
        {
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!IsInvincible)
            {
                //if (collision.gameObject.tag.Equals("Zombie"))
                //{
                //    if (!attacked)
                //    {

                //        Deactivate();
                //        GameStateManager.Instance.UpdateState(GameState.Death);
                //    }
                //}

                //if (collision.gameObject.tag.Equals("Block"))
                //{
              
                //    Deactivate();
                //    GameStateManager.Instance.UpdateState(GameState.Death);
                //}
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Powerup"))
            {
                IPowerup powerup = collision.GetComponent<IPowerup>();
                Debug.Log("Powerup Trigger = " + powerup.GetPowerupType());
                powerup.OnPowerupCollected();
                OnPowerupCollected(powerup);
            }
        }

        private void OnPowerupCollected(IPowerup powerup)
        {
            if (powerup.GetPowerupType() == PowerupType.Invincibility)
            {
                IsInvincible = true;
                StartCoroutine(DeactivateInvincibility(powerup.GetPowerupDuration()));
                LeanTween.color(gameObject, Color.cyan, 0.5f);
            }
        }

        private IEnumerator DeactivateInvincibility(int Duration)
        {
            yield return new WaitForSeconds(Duration);
            IsInvincible = false;
            LeanTween.color(gameObject, Color.white, 0.5f);
        }
    }
}