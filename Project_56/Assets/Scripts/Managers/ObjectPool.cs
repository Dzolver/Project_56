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
        public GameObject CoinWave1;
        public GameObject CoinWave2;
        public GameObject CoinWave3;

        public int PlatformCount = 3;
        public int ZombieCount = 5;
        public int BlockCount = 4;
        public int CoinWaveCount = 3;

        public Transform PooledObjectsHolder;

        [HideInInspector]
        public List<GameObject> Zombies = new List<GameObject>();

        [HideInInspector]
        public List<GameObject> Platforms = new List<GameObject>();

        [HideInInspector]
        public List<GameObject> SlideBlocks = new List<GameObject>();

        [HideInInspector]
        public List<GameObject> JumpBlocks = new List<GameObject>();

        [HideInInspector]
        public List<GameObject> CoinWaves1 = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> CoinWaves2 = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> CoinWaves3 = new List<GameObject>();

        public bool shouldExpand = false;
        private WaitForSeconds wait = new WaitForSeconds(0.001f);

        private void Start()
        {
        }

        public void StartInstantiatingObjects()
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
            if (CoinWaves1 != null)
                Total += CoinWaveCount;
            if (CoinWaves2 != null)
                Total += CoinWaveCount;
            if (CoinWaves3 != null)
                Total += CoinWaveCount;
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
            if (Platform != null)
            {
                for (int i = 0; i < PlatformCount; i++)
                {
                    GameObject gameObject = Instantiate(Platform, PooledObjectsHolder);
                    gameObject.name = "Platform -" + i;
                    gameObject.SetActive(false);
                    Platforms.Add(gameObject);
                    MyEventManager.Instance.OnObjectInstantiated.Dispatch();
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
                    MyEventManager.Instance.OnObjectInstantiated.Dispatch();

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
                    //Debug.Log("Found platform in heirarchy");
                    return Platform;
                }
            }
            if (shouldExpand)
            {
                Debug.Log("Creating platform in heirarchy");

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

     
        
    }
}