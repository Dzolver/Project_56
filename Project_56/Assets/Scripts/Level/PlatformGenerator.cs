using Project56;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject thePlatform;

    //the point in which we need to know if we need to generate any more platforms
    public Transform generationPoint;

    public float distanceBetween;

    private float platformWidth;

    // Use this for initialization
    private void Start()
    {
        platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween, transform.position.y, transform.position.z);
            Instantiate(thePlatform, transform.position, transform.rotation);
            InstantiateZombie(transform.position);
        }
    }

    private void InstantiateZombie(Vector3 position)
    {
        IZombie zombie = ObjectPool.Instance.GetZombie().GetComponent<IZombie>();
        zombie.ActivateAndSetPosition(position);
    }
}