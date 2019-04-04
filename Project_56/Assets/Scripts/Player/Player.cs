using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            MyEventManager.Instance.OnPowerupCollected.AddListener(OnPowerupCollected);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.OnPowerupCollected.RemoveListener(OnPowerupCollected);
            }
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
                if (collision.gameObject.CompareTag(GameStrings.Zombie))
                {
                    if (!attacked)
                    {
                        GameOver();
                    }
                }

                if (collision.gameObject.CompareTag(GameStrings.Block))
                {
                    GameOver();
                }
            }
            else
            {
                if (collision.gameObject.CompareTag(GameStrings.Zombie))
                {
                    GameData.Instance.AddKills();
                    collision.gameObject.GetComponent<Zombie>().Deactivate();
                }             
            }
        }

        private void GameOver()
        {
            //Deactivate();
            //SceneManager.LoadScene(3);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(GameStrings.Powerup))
            {
               collision.gameObject.GetComponent<BasePowerup>().OnPowerupCollected(); 
            }
        }

        private void OnPowerupCollected(BasePowerup powerup)
        {
            if (powerup.GetPowerupType() == PowerupType.Invincibility || powerup.GetPowerupType() == PowerupType.FastRunInvincibility)
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