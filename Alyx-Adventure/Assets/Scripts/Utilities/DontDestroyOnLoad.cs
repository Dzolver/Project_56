using UnityEngine;

namespace AlyxAdventure
{
    public class DontDestroyOnLoad : SingletonMonoBehaviour<DontDestroyOnLoad>
    {
        //[SerializeField]
        //private GameObject[] gameObjects;

        // Use this for initialization
        private void Start()
        {
            DontDestroyOnLoad(this);
            //foreach (GameObject go in gameObjects)
            //    DontDestroyOnLoad(go);
        }
    }
}