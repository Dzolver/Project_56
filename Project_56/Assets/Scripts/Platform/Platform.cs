using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlyxAdventure
{
    public enum PlatformType
    {
        VeryEasy,
        Easy,
        Average,
        Hard,
        VeryHard
    }

    public class Platform : MonoBehaviour, IPlatform
    {

        [SerializeField]
        PlatformType platformType;

        [SerializeField]
        Transform[] EnemyPoints;
        [SerializeField]
        Transform[] PowerUpPoints;
        [SerializeField]
        Transform[] CoinPoints;

        Queue<Transform> EnemySpawnPoints;
        Queue<Transform> PowerupSpawnPoints;
        Queue<Transform> CoinWaveSpawnPoints;

        private void Start()
        {
            ExtensionMethods.Shuffle(EnemyPoints);
            ExtensionMethods.Shuffle(PowerUpPoints);
            ExtensionMethods.Shuffle(CoinPoints);

            EnemySpawnPoints = new Queue<Transform>(EnemyPoints);
            PowerupSpawnPoints = new Queue<Transform>(PowerUpPoints);
            CoinWaveSpawnPoints = new Queue<Transform>(CoinPoints);
        }

        public void ActivateAndSetPosition(Vector3 position)
        {
            gameObject.SetActive(true);
            transform.SetPositionAndRotation(position, Quaternion.identity);
            // OnPowerupCollected(null);
        }

        public void Deactivate()
        {
            foreach (AbstractEnemy z in GetComponentsInChildren<AbstractEnemy>())
            {
                z.Deactivate();
            }
            foreach (CoinWave cw in GetComponentsInChildren<CoinWave>())
            {
                cw.DeactivateWave();
            }
            foreach (BasePowerup p in GetComponentsInChildren<BasePowerup>())
            {
                p.DeactivatePowerup();
            }
            gameObject.SetActive(false);
        }

        public PlatformType GetPlatformType()
        {
            return platformType;
        }

        public Transform GetEnemyPoint()
        {
            Transform t = EnemySpawnPoints.Dequeue();
            EnemySpawnPoints.Enqueue(t);
            return t;

        }

        public Transform GetPowerupPoint()
        {
            Transform t = PowerupSpawnPoints.Dequeue();
            PowerupSpawnPoints.Enqueue(t);
            return t;
        }

        public Transform GetCoinWavePoint()
        {
            Transform t = CoinWaveSpawnPoints.Dequeue();
            CoinWaveSpawnPoints.Enqueue(t);
            return t;
        }


        // public Collider2D[] InvincibilityColliders;

        //private void OnEnable()
        //{

        //        MyEventManager.Instance.OnPowerupCollected.AddListener(OnPowerupCollected);
        //        MyEventManager.Instance.OnPowerupExhausted.AddListener(OnPowerupExhausted);
        //}

        //private void OnDisable()
        //{
        //    if (MyEventManager.Instance != null )
        //    {
        //        MyEventManager.Instance.OnPowerupCollected.RemoveListener(OnPowerupCollected);
        //        MyEventManager.Instance.OnPowerupExhausted.RemoveListener(OnPowerupExhausted);
        //    }


        //}

        //private void OnPowerupCollected(BasePowerup powerup)
        //{
        //    if (powerup == null)
        //    {
        //        foreach (Collider2D collider in InvincibilityColliders)
        //            collider.isTrigger = true;
        //    }
        //    else if (powerup.GetPowerupType() == PowerupType.Invincibility || powerup.GetPowerupType() == PowerupType.FastRunInvincibility)
        //    {
        //        foreach (Collider2D collider in InvincibilityColliders)
        //            collider.isTrigger = false;
        //    }
        //}

        //private void OnPowerupExhausted(BasePowerup powerup)
        //{
        //    if (powerup.GetPowerupType() == PowerupType.Invincibility || powerup.GetPowerupType() == PowerupType.FastRunInvincibility)
        //    {
        //        foreach (Collider2D collider in InvincibilityColliders)
        //            collider.isTrigger = true;
        //    }
        //}

        //public void ActivateAndSetPosition(Vector3 position, BasePowerup powerup)
        //{
        //    gameObject.SetActive(true);
        //    transform.SetPositionAndRotation(position, Quaternion.identity);
        //    OnPowerupCollected(powerup);

        //}
    }
}