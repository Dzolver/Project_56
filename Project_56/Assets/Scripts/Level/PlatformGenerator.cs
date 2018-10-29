using Project56;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    private float platformWidth;

    public Transform startPoint;

    public Transform playerTransform;

    [SerializeField]
    GameObject CurrentPlatform, LeftPlatform, RightPlatform;

    void Start()
    {
        CurrentPlatform = ObjectPool.Instance.GetPlatform();
        CurrentPlatform.GetComponent<Platform>().ActivateAndSetPosition(startPoint.localPosition);
        platformWidth = CurrentPlatform.GetComponentInChildren<BoxCollider2D>().size.x;

        LeftPlatform = ObjectPool.Instance.GetPlatform();
        LeftPlatform.GetComponent<Platform>().ActivateAndSetPosition(new Vector2(startPoint.position.x - platformWidth, startPoint.position.y));

        RightPlatform = ObjectPool.Instance.GetPlatform();
        RightPlatform.GetComponent<Platform>().ActivateAndSetPosition(new Vector2(startPoint.position.x + platformWidth, startPoint.position.y));
    }

    void FixedUpdate()
    {
        if (playerTransform.position.x - CurrentPlatform.transform.position.x > platformWidth / 2)
            ActivateRightPlatform();
        else if (CurrentPlatform.transform.position.x - playerTransform.position.x > platformWidth / 2)
            ActivateLeftPlatform();

    }

    private void ActivateRightPlatform()
    {
        GameObject platform = ObjectPool.Instance.GetPlatform();
        platform.GetComponent<Platform>().ActivateAndSetPosition(new Vector2(RightPlatform.transform.position.x + platformWidth, RightPlatform.transform.position.y));
        LeftPlatform.GetComponent<Platform>().Deactivate();
        LeftPlatform = CurrentPlatform;
        CurrentPlatform = RightPlatform;
        RightPlatform = platform;
        InstantiateZombie(RightPlatform.transform.position);
    }

    private void ActivateLeftPlatform()
    {
        GameObject platform = ObjectPool.Instance.GetPlatform();
        platform.GetComponent<Platform>().ActivateAndSetPosition(new Vector2(LeftPlatform.transform.position.x - platformWidth, LeftPlatform.transform.position.y));
        RightPlatform.GetComponent<Platform>().Deactivate();
        RightPlatform = CurrentPlatform;
        CurrentPlatform = LeftPlatform;
        LeftPlatform = platform;
        InstantiateZombie(LeftPlatform.transform.position);
    }

    private void InstantiateZombie(Vector3 position)
    {
        IZombie zombie = ObjectPool.Instance.GetZombie().GetComponent<IZombie>();
        zombie.ActivateAndSetPosition(position);
    }
}

