using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AlyxAdventure
{
    public class Coin : MonoBehaviour {

        public void Activate()
        {
            if (!gameObject.activeInHierarchy)
                gameObject.SetActive(true);
            if (Random.Range(1, 3) == 2)
            {
                LeanTween.rotateAround(gameObject,Vector3.up, 360, Random.Range(.5f,1.5f)).setEase(LeanTweenType.linear).setLoopType(LeanTweenType.linear);
            }
        }

        public void Deactivate()
        {
            LeanTween.cancel(gameObject);
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(GameStrings.Player))
            {
                Deactivate();
                MyEventManager.Instance.OnCoinCollected.Dispatch();
            }
        }
    }

}