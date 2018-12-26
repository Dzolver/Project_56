using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

   
	void Start () {
		
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
        }
    }
}
