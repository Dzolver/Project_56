using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlyxAdventure
{
    public class ObjectPool : SingletonMonoBehaviour<ObjectPool>
    {
        public GameObject Zombie;
        public GameObject Raven;
        public GameObject[] PlatformTypes;
        public GameObject InvincibilityGO;
        public GameObject ScoreMultiplier;
        public GameObject FastRun;
        public GameObject CoinWave1;
        public GameObject CoinWave2;
        public GameObject CoinWave3;

        public int PlatformCount = 2;
        public int ZombieCount = 5;
        public int RavenCount = 5;
        public int CoinWaveCount = 3;
        public int PowerUpCount = 2;

        public Transform PooledObjectsHolder;

        [HideInInspector]
        public List<GameObject> Zombies = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> Ravens = new List<GameObject>();


        [HideInInspector]
        public List<GameObject> Platforms = new List<GameObject>();

        [HideInInspector]
        public List<GameObject> CoinWaves1 = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> CoinWaves2 = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> CoinWaves3 = new List<GameObject>();

        [HideInInspector]
        public List<GameObject> Powerups = new List<GameObject>();

        public bool shouldExpand = false;
        private WaitForSeconds wait = new WaitForSeconds(0.001f);

        private void Start()
        {
        }

        private void OnEnable()
        {
            MyEventManager.Instance.DeactivatePooledObjects.AddListener(DeactivateObjects);
        }

        private void OnDisable()
        {
            if(MyEventManager.Instance!=null)
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

            Total += PlatformCount;
            if (Zombie != null)
                Total += ZombieCount;
            if (Raven != null)
                Total += RavenCount;
            if (CoinWaves1 != null)
                Total += CoinWaveCount;
            if (CoinWaves2 != null)
                Total += CoinWaveCount;
            if (CoinWaves3 != null)
                Total += CoinWaveCount;
            if (InvincibilityGO != null)
                Total += PowerUpCount;
            if (FastRun != null)
                Total += PowerUpCount;
            if (ScoreMultiplier != null)
                Total += PowerUpCount;
            return Total;
        }

        public IEnumerator InstantiateObjects()
        {
            if (Zombie != null)
            {
                for (int i = 0; i < ZombieCount; i++)
                {
                    GameObject gameObject = Instantiate(Zombie, PooledObjectsHolder);
                    gameObject.name = "Zombie -" + i;
                    gameObject.SetActive(false);
                    Zombies.Add(gameObject);
                    MyEventManager.Instance.OnObjectInstantiated.Dispatch();
                    yield return wait;
                }
            }

            if (Raven != null)
            {
                for (int i = 0; i < RavenCount; i++)
                {
                    GameObject gameObject = Instantiate(Raven, PooledObjectsHolder);
                    gameObject.name = "Raven -" + i;
                    gameObject.SetActive(false);
                    Ravens.Add(gameObject);
                    MyEventManager.Instance.OnObjectInstantiated.Dispatch();
                    yield return wait;
                }
            }

            if (PlatformTypes != null)
            {
                for (int i = 0; i < PlatformCount * PlatformTypes.Length; i++)
                {
                    GameObject gameObject;
                    gameObject = Instantiate(PlatformTypes[i % PlatformTypes.Length], PooledObjectsHolder);
                    gameObject.name = "Platform - " + i;
                    gameObject.SetActive(false);
                    Platforms.Add(gameObject);
                    MyEventManager.Instance.OnObjectInstantiated.Dispatch();
                    yield return wait;
                }
            }

            if (CoinWave1 != null)
            {
                for (int i = 0; i < CoinWaveCount; i++)
                {
                    GameObject gameObject = Instantiate(CoinWave1, PooledObjectsHolder);
                    gameObject.name = "CoinWave 1-" + i;
                    gameObject.SetActive(false);
                    CoinWaves1.Add(gameObject);
                    MyEventManager.Instance.OnObjectInstantiated.Dispatch();
                    yield return wait;
                }
            }
            if (CoinWave2 != null)
            {
                for (int i = 0; i < CoinWaveCount; i++)
                {
                    GameObject gameObject = Instantiate(CoinWave2, PooledObjectsHolder);
                    gameObject.name = "CoinWave 2-" + i;
                    gameObject.SetActive(false);
                    CoinWaves2.Add(gameObject);
                    MyEventManager.Instance.OnObjectInstantiated.Dispatch();
                    yield return wait;
                }
            }
            if (CoinWave3 != null)
            {
                for (int i = 0; i < CoinWaveCount; i++)
                {
                    GameObject gameObject = Instantiate(CoinWave3, PooledObjectsHolder);
                    gameObject.name = "CoinWave 3-" + i;
                    gameObject.SetActive(false);
                    CoinWaves3.Add(gameObject);
                    MyEventManager.Instance.OnObjectInstantiated.Dispatch();
                    yield return wait;
                }
            }

            for (int i = 0; i < PowerUpCount * 3; i++)
            {
                GameObject gameObject;
                if (i < 2)
                    gameObject = Instantiate(InvincibilityGO, PooledObjectsHolder);
                else if (i < 4)
                    gameObject = Instantiate(FastRun, PooledObjectsHolder);
                else
                    gameObject = Instantiate(ScoreMultiplier, PooledObjectsHolder);
                gameObject.name = "PowerUp" + i;
                gameObject.SetActive(false);
                Powerups.Add(gameObject);
                MyEventManager.Instance.OnObjectInstantiated.Dispatch();
                yield return wait;
            }
        }

        public GameObject GetZombie()
        {
            //Perform normal return of the selected cube from selected queue
            foreach (GameObject zombie in Zombies)
            {
                if (!zombie.activeInHierarchy)
                {
                    return zombie;
                }
            }
            //If there are no deactivated objects, instantiate a new one and return that
            //Increase count in the start if this case arrives while testing
            if (shouldExpand)
            {
                GameObject gameObject = Instantiate(Zombie);
                gameObject.SetActive(false);
                Zombies.Add(gameObject);
                return gameObject;
            }
            else
            {
                return null;
            }
        }

        public GameObject GetRaven()
        {
            //Perform normal return of the selected cube from selected queue
            foreach (GameObject raven in Ravens)
            {
                if (!raven.activeInHierarchy)
                {
                    return raven;
                }
            }
            //If there are no deactivated objects, instantiate a new one and return that
            //Increase count in the start if this case arrives while testing
            if (shouldExpand)
            {
                GameObject gameObject = Instantiate(Raven);
                gameObject.SetActive(false);
                Ravens.Add(gameObject);
                return gameObject;
            }
            else
            {
                return null;
            }
        }

        public GameObject GetPlatform(PlatformType platformType)
        {            
            foreach (GameObject Platform in Platforms)
            {
                if (!Platform.activeInHierarchy && Platform.GetComponent<Platform>().GetPlatformType() == platformType)
                {
                    return Platform;
                }
            }
            foreach (GameObject Platform in Platforms)
            {
                if (!Platform.activeInHierarchy)
                {
                    return Platform;
                }
            }
            return null;

        }

        public GameObject GetPowerUp(PowerupType type)
        {
            foreach (GameObject go in Powerups)
            {
                if (!go.activeInHierarchy && go.GetComponent<BasePowerup>().GetPowerupType() == type)
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

        public GameObject GetCoinWave(int num)
        {
            List<GameObject> CoinWaves;
            GameObject CoinWave;

            switch (num)
            {
                case 1:
                    CoinWaves = CoinWaves1;
                    CoinWave = CoinWave1;
                    break;
                case 2:
                    CoinWaves = CoinWaves2;
                    CoinWave = CoinWave2;
                    break;
                case 3:
                    CoinWaves = CoinWaves3;
                    CoinWave = CoinWave3;
                    break;
                default:
                    CoinWaves = CoinWaves1;
                    CoinWave = CoinWave1;
                    break;
            }
            foreach (GameObject cw in CoinWaves)
            {
                if (!cw.activeInHierarchy)
                {
                    return cw;
                }
            }
            if (shouldExpand)
            {
                GameObject gameObject = Instantiate(CoinWave);
                gameObject.SetActive(false);
                CoinWaves.Add(gameObject);
                return gameObject;
            }
            else
            {
                return null;
            }
        }

        private void DeactivateObjects()
        {
            foreach (GameObject zombie in Zombies)
            {
                zombie.SetActive(false);
            }
            foreach (GameObject Platform in Platforms)
            {
                Platform.SetActive(false);
            }
            foreach (GameObject go in Powerups)
            {
                go.SetActive(false);
            }
            foreach (GameObject cw in CoinWaves1)
            {
                cw.SetActive(false);
            }
            foreach (GameObject cw in CoinWaves2)
            {
                cw.SetActive(false);
            }
            foreach (GameObject cw in CoinWaves3)
            {
                cw.SetActive(false);
            }

            foreach (GameObject r in Ravens)
            {
                r.SetActive(false);
            }
        }
    }
}