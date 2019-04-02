using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Project56
{
    public class SplashScreen : MonoBehaviour
    {
        int Counter = 0;
        // Use this for initialization
        void Start()
        {
            ObjectPool.Instance.StartInstantiatingObjects();
        }

        private void OnEnable()
        {
            MyEventManager.Instance.OnObjectInstantiated.AddListener(OnObjectInstantiated);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.OnObjectInstantiated.RemoveListener(OnObjectInstantiated);
            }

        }

        private void OnObjectInstantiated()
        {
            Counter++;
            if (Counter >= ObjectPool.Instance.GetTotalObjectCount())
                SceneManager.LoadScene(1);
        }

        //IEnumerator LoadMainMenu()
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    SceneManager.LoadScene(1);

        //}
    }

}