using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public class ObjectPool : SingletonMonoBehaviour<ObjectPool>
    {
        public GameObject Zombie;
        public GameObject Platform;

        public int PlatformCount = 3;
        public int ZombieCount = 5;

        public Transform PooledObjectsHolder;

        [HideInInspector]
        public List<GameObject> Zombies = new List<GameObject>();

        [HideInInspector]
        public List<GameObject> Platforms = new List<GameObject>();

        public bool shouldExpand = false;
        private WaitForSeconds wait = new WaitForSeconds(0.001f);

        private void Start()
        {
            StartCoroutine(InstantiateObjects());
        }

        public int GetTotalObjectCount()
        {
            int Total = 0;

            if (Zombie != null)
                Total += ZombieCount;
            if (Platform != null)
                Total += PlatformCount;
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
                    yield return wait;
                }
            }
            if (Platform != null)
            {
                for (int i = 0; i < PlatformCount; i++)
                {
                    GameObject gameObject = Instantiate(Platform, PooledObjectsHolder);
                    gameObject.name = "Platform -" + i;
                    gameObject.SetActive(false);
                    Platforms.Add(gameObject);
                    yield return wait;
                }
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

        public GameObject GetPlatform()
        {
            //Perform normal return of the selected cube from selected queue
            foreach (GameObject Platform in Platforms)
            {
                if (!Platform.activeInHierarchy)
                {
                    return Platform;
                }
            }
            //If there are no deactivated objects, instantiate a new one and return that
            //Increase count in the start if this case arrives while testing
            if (shouldExpand)
            {
                Debug.Log("No  Platform");

                GameObject gameObject = Instantiate(Platform);
                gameObject.SetActive(false);
                Platforms.Add(gameObject);
                return gameObject;
            }
            else
            {
                Debug.Log("Getting Null");

                return null;
            }
        }
    }
}