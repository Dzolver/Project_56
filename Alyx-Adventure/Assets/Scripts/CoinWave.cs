using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AlyxAdventure
{
    public class CoinWave : MonoBehaviour
    {
        public List<Coin> Coins;

        private void Start()
        {
            Coins = new List<Coin>(GetComponentsInChildren<Coin>());
        }

        public void ActivateAndSetPosition(Vector2 position, Quaternion rotation, Transform parent)
        {
            gameObject.transform.SetPositionAndRotation(position, rotation);
            gameObject.SetActive(true);
            gameObject.transform.SetParent(parent);
            foreach (Coin coin in Coins)
            {
                coin.Activate();
            }
        }

        public void DeactivateWave()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(GameStrings.MainCamera))
            {
                DeactivateWave();
            }
        }
    }

}