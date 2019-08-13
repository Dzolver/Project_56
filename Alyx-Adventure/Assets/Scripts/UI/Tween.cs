using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween : MonoBehaviour
{
    private void Start()
    {
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), .5f).setLoopPingPong();
    }

}
