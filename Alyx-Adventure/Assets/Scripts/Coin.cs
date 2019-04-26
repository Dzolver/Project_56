using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AlyxAdventure
{
    public class Coin : MonoBehaviour {

        public Transform HudCoin;
        private Vector3 OriginalPosition;
        private void Start()
        {
            OriginalPosition = transform.localPosition;
            Activate();
        }

        public void Activate()
        {
            transform.localPosition = OriginalPosition;
            transform.localScale = Vector2.one;
            if (!gameObject.activeInHierarchy)
                gameObject.SetActive(true);
            if (HudCoin == null)
                HudCoin = GameObject.FindGameObjectWithTag(GameStrings.HudCoin).transform;
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
                //Deactivate();
                transform.localScale = Vector2.one / 2;
                LeanTween.move(gameObject, HudCoin.position, .3f).setEase(LeanTweenType.easeOutQuad).setOnComplete(OnCoinReached);
                MyEventManager.Instance.OnCoinCollected.Dispatch();
            }
        }

        private void OnCoinReached()
        {
            Deactivate();
        }



    }

}