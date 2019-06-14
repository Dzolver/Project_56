using AlyxAdventure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFragmentBase : MonoBehaviour
{

    private void FixedUpdate()
    {
        Collider2D horizontalObject = Physics2D.OverlapBox(transform.position, new Vector2(4, .1f), 0f);
        Collider2D verticalObject = Physics2D.OverlapBox(transform.position, new Vector2(.1f, 4), 0f);
        if (horizontalObject.CompareTag(GameStrings.Ground) || 
            verticalObject.CompareTag(GameStrings.Platform) || verticalObject.CompareTag(GameStrings.JumpBlock))
        {
            Debug.Log("Moving forward " + gameObject.name);
            transform.position = new Vector2(transform.position.x + (int)GameData.Instance.direction, transform.position.y);
        }
    }

    public void ActivateAndSetPosition(Vector2 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
        LeanTween.rotateAround(gameObject, Vector3.up, 180, 1f).setLoopPingPong();
    }

    public void DeactivateFragment()
    {
        LeanTween.cancel(gameObject);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameStrings.Player))
        {
            OnFragmentCollected();
        }
    }

    public void OnFragmentCollected()
    {
        MyEventManager.Instance.OnFragmentCollected.Dispatch(this);
        DeactivateFragment();
    }

}