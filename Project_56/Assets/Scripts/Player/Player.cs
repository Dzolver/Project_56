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
                if (collision.gameObject.tag.Equals("Zombie"))
                {
                    if (!attacked)
                    {
                        GameOver();
                    }
                }

                if (collision.gameObject.tag.Equals("Block"))
                {
                    GameOver();
                }
            }
            else
            {
                if (collision.gameObject.tag.Equals("Zombie"))
                {
                    GameData.Instance.AddKills();
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
            if (collision.CompareTag("Powerup"))
            {
                IPowerup powerup = collision.GetComponent<IPowerup>();
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