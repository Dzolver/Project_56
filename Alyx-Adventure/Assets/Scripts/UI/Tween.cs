using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween : MonoBehaviour
{

    void Start()
    {
        LeanTween.scale(gameObject, new Vector3(.5f, .5f, 0), .5f).setLoopPingPong();
    }

}
