﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public class Platform : MonoBehaviour, IPlatform
    {
        [SerializeField]
        int platformId;

        public void ActivateAndSetPosition(Vector3 position)
        {
            gameObject.SetActive(true);
            transform.SetPositionAndRotation(position, Quaternion.identity);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public int GetPlatformId()
        {
            return platformId;
        }
    }
}