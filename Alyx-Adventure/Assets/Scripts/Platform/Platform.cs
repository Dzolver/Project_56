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

        private void OnEnable()
        {
            MyEventManager.Instance.OnGotEnemyParent.AddListener(OnGotEnemyParent);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.OnGotEnemyParent.RemoveListener(OnGotEnemyParent);
            }
        }

        private void Start()
        {
            PowerUpPoints = new List<Transform>(PowerupParent.GetComponentsInChildren<Transform>());
            PowerUpPoints.Remove(PowerupParent);
            PowerUpPoints.Shuffle();  
            PowerupSpawnPoints = new Queue<Transform>(PowerUpPoints);

            CoinPoints = new List<Transform>(CoinParent.GetComponentsInChildren<Transform>());
            CoinPoints.Remove(CoinParent);
            CoinWaveSpawnPoints = new Queue<Transform>(CoinPoints);
            CoinPoints.Shuffle();

            if (ZombieParent != null)
            {
                ZombiePoints = new List<Transform>(ZombieParent.GetComponentsInChildren<Transform>());
                ZombiePoints.Remove(ZombieParent);
                ZombiePoints.Shuffle();
                ZombieSpawnPoints = new Queue<Transform>(ZombiePoints);
            }

            if (RavenParent != null)
            {

                RavenPoints = new List<Transform>(RavenParent.GetComponentsInChildren<Transform>());
                RavenPoints.Remove(RavenParent);
                RavenPoints.Shuffle();
                RavenSpawnPoints = new Queue<Transform>(RavenPoints);
            }

        }


        private void OnGotEnemyParent(AbstractEnemy enemy, Platform platform)
        {
            if (platform == this)
            {
                if (enemy.GetEnemyType() == AbstractEnemy.EnemyType.Zombie)
                    StartCoroutine(GetPointAndActivateZombie((Zombie)enemy));
                else
                    StartCoroutine(GetPointAndActivateRaven((Raven)enemy));
            }
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

        private IEnumerator GetPointAndActivateZombie(Zombie zombie)
        {
            if (ZombieParent != null)
            {
                Transform t;
                do
                {
                    t = ZombieSpawnPoints.Dequeue();
                    ZombieSpawnPoints.Enqueue(t);
                    yield return new WaitForEndOfFrame();
                }
                while (Mathf.Abs(t.position.x - GameData.Instance.theRunnerTransform.position.x) < 14f);

                zombie.ActivateAndSetPosition(t.position, transform);
            }

        }

        private IEnumerator GetPointAndActivateRaven(Raven raven)
        {
            if (RavenParent != null)
            {
                Transform t;
                do
                {
                    t = RavenSpawnPoints.Dequeue();
                    RavenSpawnPoints.Enqueue(t);
                    yield return new WaitForEndOfFrame();
                }
                while (Mathf.Abs(t.position.x - GameData.Instance.theRunnerTransform.position.x) < 14f);

                raven.ActivateAndSetPosition(t.position, transform);
            }

        }

        public Transform GetPowerupPoint()
        {
            Transform t = PowerupSpawnPoints.Dequeue();
            PowerupSpawnPoints.Enqueue(t);
            Debug.Log(t.position);
            return t;
        }

        public Transform GetCoinWavePoint()
        {
            Transform t = CoinWaveSpawnPoints.Dequeue();
            CoinWaveSpawnPoints.Enqueue(t);
            return t;
        }


        public PlatformType GetPlatformType()
        {
            return platformType;
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