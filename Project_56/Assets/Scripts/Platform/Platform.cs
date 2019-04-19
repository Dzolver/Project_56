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

        public Transform ZombieParent;
        public Transform RavenParent;
        public Transform PowerupParent;
        public Transform CoinParent;

        List<Transform> ZombiePoints;
        List<Transform> RavenPoints;
        List<Transform> PowerUpPoints;
        List<Transform> CoinPoints;

        Queue<Transform> ZombieSpawnPoints;
        Queue<Transform> RavenSpawnPoints;
        Queue<Transform> PowerupSpawnPoints;
        Queue<Transform> CoinWaveSpawnPoints;

        private void Start()
        {
            ZombiePoints = new List<Transform>(ZombieParent.GetComponentsInChildren<Transform>());
            RavenPoints = new List<Transform>(RavenParent.GetComponentsInChildren<Transform>());
            PowerUpPoints = new List<Transform>(PowerupParent.GetComponentsInChildren<Transform>());
            CoinPoints = new List<Transform>(CoinParent.GetComponentsInChildren<Transform>());

            ZombiePoints.Shuffle();
            PowerUpPoints.Shuffle();
            CoinPoints.Shuffle();
            RavenPoints.Shuffle();

            ZombieSpawnPoints = new Queue<Transform>(ZombiePoints);
            PowerupSpawnPoints = new Queue<Transform>(PowerUpPoints);
            CoinWaveSpawnPoints = new Queue<Transform>(CoinPoints);
            RavenSpawnPoints = new Queue<Transform>(RavenPoints);
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

        public Transform GetZombiePoint()
        {
            Transform t = ZombieSpawnPoints.Dequeue();
            ZombieSpawnPoints.Enqueue(t);
            return t;

        }

        public Transform GetRavenPoint()
        {
            Transform t = RavenSpawnPoints.Dequeue();
            RavenSpawnPoints.Enqueue(t);
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