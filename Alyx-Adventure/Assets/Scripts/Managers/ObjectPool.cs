using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlyxAdventure
{
    public class ObjectPool : SingletonMonoBehaviour<ObjectPool>
    {
        public Zombie Zombie;
        public Raven Raven;
        public BasePowerup InvincibilityGO;
        public BasePowerup ScoreMultiplier;
        public BasePowerup FastRun;
        public Platform[] PlatformTypes;
        public CoinWave[] CoinWaveTypes;
        public CollectableFragmentBase[] FragmentTypes;

        public int PlatformCount;
        public int ZombieCount;
        public int RavenCount;
        public int CoinWaveCount;
        public int PowerUpCount;
        public int FragmentCount;

        public Transform PooledObjectsHolder;

        [HideInInspector]
        public List<Zombie> Zombies = new List<Zombie>();

        [HideInInspector]
        public List<Raven> Ravens = new List<Raven>();

        [HideInInspector]
        public List<Platform> Platforms = new List<Platform>();

        [HideInInspector]
        public List<CoinWave> CoinWaves = new List<CoinWave>();

        [HideInInspector]
        public List<CollectableFragmentBase> Fragments = new List<CollectableFragmentBase>();

        [HideInInspector]
        public List<BasePowerup> Powerups = new List<BasePowerup>();

        public bool shouldExpand = false;
        private WaitForSeconds wait = new WaitForSeconds(0.01f);

        private void OnEnable()
        {
            MyEventManager.Instance.DeactivatePooledObjects.AddListener(DeactivateObjects);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.DeactivatePooledObjects.RemoveListener(DeactivateObjects);
            }
        }

        public void StartInstantiatingObjects()
        {
            StartCoroutine(InstantiateObjects());
        }

        public int GetTotalObjectCount()
        {
            int Total = 0;

            if (PlatformTypes != null)
                Total += PlatformCount * PlatformTypes.Length;
            if (Zombie != null)
                Total += ZombieCount;
            if (Raven != null)
                Total += RavenCount;
            if (CoinWaveTypes != null)
                Total += CoinWaveCount * CoinWaveTypes.Length;
            if (InvincibilityGO != null)
                Total += PowerUpCount;
            if (FastRun != null)
                Total += PowerUpCount;
            if (ScoreMultiplier != null)
                Total += PowerUpCount;
            if (FragmentTypes != null)
                Total += FragmentCount * FragmentTypes.Length;
            return Total;
        }

        public IEnumerator InstantiateObjects()
        {
            if (Zombie != null)
            {
                for (int i = 0; i < ZombieCount; i++)
                {
                    GameObject gameObject = Instantiate(Zombie.gameObject, PooledObjectsHolder);
                    gameObject.GetComponent<Zombie>().SetMoveSpeed(2f);
                    gameObject.name = "Zombie -" + i;
                    gameObject.SetActive(false);
                    Zombies.Add(gameObject.GetComponent<Zombie>());
                    MyEventManager.Instance.OnObjectInstantiated.Dispatch();
                    yield return wait;
                }
            }

            if (Raven != null)
            {
                for (int i = 0; i < RavenCount; i++)
                {
                    GameObject gameObject = Instantiate(Raven.gameObject, PooledObjectsHolder);
                    gameObject.GetComponent<Raven>().SetMoveSpeed(3f);
                    gameObject.name = "Raven -" + i;
                    gameObject.SetActive(false);
                    Ravens.Add(gameObject.GetComponent<Raven>());
                    MyEventManager.Instance.OnObjectInstantiated.Dispatch();
                    yield return wait;
                }
            }

            if (PlatformTypes != null)
            {
                for (int i = 0; i < PlatformCount * PlatformTypes.Length; i++)
                {
                    GameObject gameObject;
                    gameObject = Instantiate(PlatformTypes[i % PlatformTypes.Length].gameObject, PooledObjectsHolder);
                    gameObject.name = "Platform - " + i;
                    gameObject.SetActive(false);
                    Platforms.Add(gameObject.GetComponent<Platform>());
                    MyEventManager.Instance.OnObjectInstantiated.Dispatch();
                    yield return wait;
                }
            }

            if (CoinWaveTypes != null)
            {
                for (int i = 0; i < CoinWaveCount * CoinWaveTypes.Length; i++)
                {
                    GameObject gameObject;
                    gameObject = Instantiate(CoinWaveTypes[i % CoinWaveTypes.Length].gameObject, PooledObjectsHolder);
                    gameObject.name = "CoinWave " + i;
                    gameObject.SetActive(false);
                    CoinWaves.Add(gameObject.GetComponent<CoinWave>());
                    MyEventManager.Instance.OnObjectInstantiated.Dispatch();
                    yield return wait;
                }
                CoinWaves.Shuffle();
            }

            if (FragmentTypes != null)
            {
                for (int i = 0; i < FragmentCount * FragmentTypes.Length; i++)
                {
                    GameObject gameObject;
                    gameObject = Instantiate(FragmentTypes[i % FragmentTypes.Length].gameObject, PooledObjectsHolder);
                    gameObject.name = "Fragment " + i;
                    gameObject.SetActive(false);
                    Fragments.Add(gameObject.GetComponent<CollectableFragmentBase>());
                    MyEventManager.Instance.OnObjectInstantiated.Dispatch();
                    yield return wait;
                }
            }

            for (int i = 0; i < PowerUpCount * 3; i++)
            {
                GameObject gameObject;
                if (i < 2)
                    gameObject = Instantiate(InvincibilityGO.gameObject, PooledObjectsHolder);
                else if (i < 4)
                    gameObject = Instantiate(FastRun.gameObject, PooledObjectsHolder);
                else
                    gameObject = Instantiate(ScoreMultiplier.gameObject, PooledObjectsHolder);
                gameObject.name = "PowerUp" + i;
                gameObject.SetActive(false);
                Powerups.Add(gameObject.GetComponent<BasePowerup>());
                MyEventManager.Instance.OnObjectInstantiated.Dispatch();
                yield return wait;
            }
        }

        public Zombie GetZombie()
        {
            //Perform normal return of the selected cube from selected queue
            foreach (Zombie zombie in Zombies)
            {
                if (!zombie.gameObject.activeInHierarchy)
                {
                    return zombie;
                }
            }
            //If there are no deactivated objects, instantiate a new one and return that
            //Increase count in the start if this case arrives while testing
            if (shouldExpand)
            {
                GameObject gameObject = Instantiate(Zombie.gameObject);
                gameObject.SetActive(false);
                Zombies.Add(gameObject.GetComponent<Zombie>());
                return gameObject.GetComponent<Zombie>();
            }
            else
            {
                return null;
            }
        }

        public Raven GetRaven()
        {
            foreach (Raven raven in Ravens)
            {
                if (!raven.gameObject.activeInHierarchy)
                {
                    return raven.GetComponent<Raven>();
                }
            }
            if (shouldExpand)
            {
                GameObject gameObject = Instantiate(Raven.gameObject);
                gameObject.SetActive(false);
                Ravens.Add(gameObject.GetComponent<Raven>());
                return gameObject.GetComponent<Raven>();
            }
            else
            {
                return null;
            }
        }

        public Platform GetPlatform(PlatformType platformType)
        {
            foreach (Platform platform in Platforms)
            {
                if (!platform.gameObject.activeInHierarchy && platform.GetPlatformType() == platformType)
                {
                    return platform;
                }
            }
            foreach (Platform platform in Platforms)
            {
                if (!platform.gameObject.activeInHierarchy)
                {
                    return platform;
                }
            }
            return null;

        }

        public BasePowerup GetPowerUp(PowerupType type)
        {
            foreach (BasePowerup go in Powerups)
            {
                if (!go.gameObject.activeInHierarchy && go.GetPowerupType() == type)
                {
                    return go;
                }
            }
            return null;

            //if (shouldExpand)
            //{
            //    GameObject gameObject = Instantiate(powerup);
            //    gameObject.SetActive(false);
            //    Powerups.Add(gameObject);
            //    return gameObject;
            //}

        }

        public CollectableFragmentBase GetFragment()
        {
            int random = Random.Range(0, Fragments.Count);
            if (!Fragments[random].gameObject.activeInHierarchy)
                return Fragments[random];

            foreach (CollectableFragmentBase f in Fragments)
            {
                if (!f.gameObject.activeInHierarchy)
                {
                    return f.GetComponent<CollectableFragmentBase>();
                }
            }
            return null;
        }

        public CoinWave GetCoinWave()
        {
            foreach (CoinWave cw in CoinWaves)
            {
                if (!cw.gameObject.activeInHierarchy)
                {
                    return cw.GetComponent<CoinWave>();
                }
            }
            return null;
        }

        private void DeactivateObjects()
        {
            foreach (Zombie zombie in Zombies)
            {
                zombie.Deactivate();
            }
            foreach (Raven r in Ravens)
            {
                r.Deactivate();
            }
            foreach (BasePowerup go in Powerups)
            {
                go.DeactivatePowerup();
            }
            foreach (CoinWave cw in CoinWaves)
            {
                cw.DeactivateWave();
            }
            foreach (Platform Platform in Platforms)
            {
                Platform.Deactivate();
            }

        }
    }
}