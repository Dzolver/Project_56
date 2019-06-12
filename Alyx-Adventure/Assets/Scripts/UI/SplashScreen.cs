using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AlyxAdventure
{
    public class SplashScreen : MonoBehaviour
    {
        public Slider slider;
        int Counter = 0;
       
        void Start()
        {
            Application.targetFrameRate = 60;
            slider.minValue = 0;
            slider.maxValue = ObjectPool.Instance.GetTotalObjectCount();
            slider.value = Counter;
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
            slider.value = Counter;
            if (Counter >= ObjectPool.Instance.GetTotalObjectCount())
                SceneManager.LoadScene(1);
        }

    }

}