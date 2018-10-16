using UnityEngine;

namespace Project56
{
    public class DontDestroyOnLoad : SingletonMonoBehaviour<DontDestroyOnLoad>
    {
        //[SerializeField]
        //private GameObject[] gameObjects;

        // Use this for initialization
        private void Start()
        {
            Debug.Log(name);
            DontDestroyOnLoad(this);
            //foreach (GameObject go in gameObjects)
            //    DontDestroyOnLoad(go);
        }
    }
}