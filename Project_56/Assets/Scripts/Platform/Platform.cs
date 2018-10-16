using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public class Platform : MonoBehaviour, IPlatform
    {
        public void ActivateAndSetPosition(Vector3 position)
        {
            transform.SetPositionAndRotation(position, Quaternion.identity);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            ObjectPool.Instance.ReturnPlatformToStack(gameObject);
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