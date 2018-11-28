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
        public GameObject Jump_Block;
        public GameObject Slide_Block;

        public int PlatformCount = 3;
        public int ZombieCount = 5;
        public int BlockCount = 4;

        public Transform PooledObjectsHolder;

        [HideInInspector]
        public List<GameObject> Zombies = new List<GameObject>();

        [HideInInspector]
        public List<GameObject> Platforms = new List<GameObject>();

        [HideInInspector]
        public List<GameObject> SlideBlocks = new List<GameObject>();

        [HideInInspector]
        public List<GameObject> JumpBlocks = new List<GameObject>();

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
            if (Jump_Block != null)
                Total += BlockCount;
            if (SlideBlocks != null)
                Total += BlockCount;
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
            if (Slide_Block != null)
            {
                for (int i = 0; i < BlockCount; i++)
                {
                    GameObject gameObject = Instantiate(Slide_Block, PooledObjectsHolder);
                    gameObject.name = "Slide Block -" + i;
                    gameObject.SetActive(false);
                    SlideBlocks.Add(gameObject);
                    yield return wait;
                }
            }
            if (Jump_Block != null)
            {
                for (int i = 0; i < BlockCount; i++)
                {
                    GameObject gameObject = Instantiate(Jump_Block, PooledObjectsHolder);
                    gameObject.name = "Jump Block -" + i;
                    gameObject.SetActive(false);
                    JumpBlocks.Add(gameObject);
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

        public GameObject GetSlideBlock()
        {
            //Perform normal return of the selected cube from selected queue
            foreach (GameObject block in SlideBlocks)
            {
                if (!block.activeInHierarchy)
                {
                    return block;
                }
            }
            if (shouldExpand)
            {
                GameObject gameObject = Instantiate(Slide_Block);
                gameObject.SetActive(false);
                SlideBlocks.Add(gameObject);
                return gameObject;
            }
            else
            {
                return null;
            }
        }

        public GameObject GetJumpBlock()
        {
            //Perform normal return of the selected cube from selected queue
            foreach (GameObject block in JumpBlocks)
            {
                if (!block.activeInHierarchy)
                {
                    return block;
                }
            }
            if (shouldExpand)
            {
                GameObject gameObject = Instantiate(Jump_Block);
                gameObject.SetActive(false);
                JumpBlocks.Add(gameObject);
                return gameObject;
            }
            else
            {
                return null;
            }
        }

        private void OnEnable()
        {
            MyEventManager.Instance.OnGameStateChanged.AddListener(OnGameStateChanged);
        }

        private void OnGameStateChanged()
        {
            if (GameStateManager.Instance.CurrentState == GameState.Death
                || GameStateManager.Instance.CurrentState == GameState.MainMenu)
            {
                foreach (GameObject platform in Platforms)
                    platform.SetActive(false);

                foreach (GameObject zombie in Zombies)
                    zombie.SetActive(false);

                foreach (GameObject block in JumpBlocks)
                    block.SetActive(false);

                foreach (GameObject block in SlideBlocks)
                    block.SetActive(false);
            }
        }

        private void OnDisable()
        {
            MyEventManager.Instance.OnGameStateChanged.RemoveListener(OnGameStateChanged);
        }
    }
}