using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AlyxAdventure
{
    public class Coin : MonoBehaviour {

        void Start() {
           
        }

        public void Activate()
        {
            if (!gameObject.activeInHierarchy)
                gameObject.SetActive(true);
            if (Random.Range(1, 3) == 2)
            {
                Debug.Log("Activating");
                LeanTween.rotateAround(gameObject,Vector3.up, 360, Random.Range(.5f,1.5f)).setEase(LeanTweenType.linear).setLoopType(LeanTweenType.linear);
            }
        }

        public void ActivateAndSetPosition(Vector2 position)
        {
            gameObject.SetActive(true);
            gameObject.transform.SetPositionAndRotation(position, Quaternion.identity);
            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                gameObject.SetActive(false);
                MyEventManager.Instance.OnCoinCollected.Dispatch();
            }
        }
    }

}