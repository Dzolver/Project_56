using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public class HUD : MonoBehaviour
    {
        public void OnJumpClicked()
        {
            Debug.Log("Jumping");
            MyEventManager.Instance.OnJumpClicked.Dispatch();
        }

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}