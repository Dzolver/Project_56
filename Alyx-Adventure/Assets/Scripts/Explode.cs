using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public void ExplodeObject(Vector3 position)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = position;
        LeanTween.move(gameObject, new Vector3(position.x,position.y + 5,position.z), .8f);
    }

    public void Deactivate()
    {
        LeanTween.cancel(gameObject);
        gameObject.SetActive(false);
    }
}
