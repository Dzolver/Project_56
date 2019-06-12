using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween : MonoBehaviour
{
    private void Start()
    {
        LeanTween.rotateAround(gameObject.GetComponent<Transform>().gameObject, Vector3.up, 180f, 0.5f).setLoopPingPong();
        // LeanTween.scale(gameObject, new Vector3(.5f, .5f, 0), .5f).setLoopPingPong();
    }

}
