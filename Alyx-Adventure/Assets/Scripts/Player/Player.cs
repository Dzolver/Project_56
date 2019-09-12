using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlyxAdventure
{
    [RequireComponent(typeof(PlayerController))]
    public class Player : MonoBehaviour, IPlayer
    {
        public bool attacked;
        private bool IsInvincible = false;

        PlayerController playerController;

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

        private void Start()
        {
            playerController = GetComponent<PlayerController>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(GameStrings.Powerup))
            {
                collision.gameObject.GetComponent<BasePowerup>().OnPowerupCollected();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!IsInvincible)
            {
                OnCollision(collision);
            }
            else
            {
                if (collision.gameObject.CompareTag(GameStrings.Enemy))
                {
                    CameraShake.Instance.shakeDuration = .2f;
                    ScoreManager.Instance.AddKills();
                    collision.gameObject.GetComponent<AbstractEnemy>().Deactivate();

                }
                else if (collision.gameObject.CompareTag(GameStrings.JumpBlock))
                {
                    playerController.grounded = true;
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!IsInvincible)
            {
                OnCollision(collision);
            }
        }

        private void OnCollision(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameStrings.Enemy))
            {
                if (!attacked)
                    GameOver();
            }

            else if (collision.gameObject.CompareTag(GameStrings.JumpBlock) || collision.gameObject.CompareTag(GameStrings.SlideBlock))
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            if (GameTimeManager.Instance.CanDie)
            {
                Deactivate();
                MyEventManager.Instance.OnGameOver.Dispatch();
            }
        }

        private void SlowDownTime(float value)
        {
            Time.timeScale = value;
        }

        private void NormalizeTime()
        {
            Time.timeScale = 1.0f;
        }

        public void ActivateAndSetPosition(Vector3 vector3)
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
        {
            LeanTween.color(gameObject, Color.white, 0.1f);
            gameObject.SetActive(false);
            gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        private void OnPowerupCollected(BasePowerup powerup)
        {
            if (powerup.GetPowerupType() == PowerupType.Invincibility || powerup.GetPowerupType() == PowerupType.FastRunInvincibility)
            {
                IsInvincible = true;
                //LeanTween.color(gameObject, Color.cyan, 0.5f);
                GetComponentInChildren<ParticleSystem>().Play();
                Invoke("StopParticles", powerup.GetPowerupDuration() - 1.5f);
            }
        }

        private void StopParticles()
        {
            GetComponentInChildren<ParticleSystem>().Stop();
        }

        private void OnPowerupExhausted(BasePowerup powerup)
        {
            if (powerup.GetPowerupType() == PowerupType.Invincibility || powerup.GetPowerupType() == PowerupType.FastRunInvincibility)
            {
                IsInvincible = false;
                //LeanTween.color(gameObject, Color.white, 0.5f);               
            }
        }

    }
}