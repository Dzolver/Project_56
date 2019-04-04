using Project56;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform startPoint;
    public float platformWidth;
    [SerializeField]
    private Platform CurrentPlatform, LeftPlatform, RightPlatform;

    private void Start()
    {
        if (CurrentPlatform == null)
        {
            CurrentPlatform = ObjectPool.Instance.GetPlatform().GetComponent<Platform>();
            platformWidth = CurrentPlatform.GetComponentInChildren<BoxCollider2D>().size.x;
            CurrentPlatform.ActivateAndSetPosition(startPoint.localPosition);
        }
        else
            CurrentPlatform.SetActive(true);
        if (LeftPlatform == null)
        {
            LeftPlatform = ObjectPool.Instance.GetPlatform().GetComponent<Platform>();
            LeftPlatform.ActivateAndSetPosition(new Vector2(startPoint.position.x - platformWidth, startPoint.position.y));
        }
        else
            LeftPlatform.SetActive(true);
        if (RightPlatform == null)
        {
            RightPlatform = ObjectPool.Instance.GetPlatform().GetComponent<Platform>();
            RightPlatform.ActivateAndSetPosition(new Vector2(startPoint.position.x + platformWidth, startPoint.position.y));
        }
        else
            RightPlatform.SetActive(true);
    }

    private void FixedUpdate()
    {

        if (GameData.Instance.theRunnerTransform.position.x - CurrentPlatform.transform.position.x > platformWidth / 2)
            ActivateRightPlatform();
        else if (CurrentPlatform.transform.position.x - GameData.Instance.theRunnerTransform.position.x > platformWidth / 2)
            ActivateLeftPlatform();

    }

    private void ActivateRightPlatform()
    {
        Platform platform = ObjectPool.Instance.GetPlatform().GetComponent<Platform>();
        platform.ActivateAndSetPosition(new Vector2(RightPlatform.transform.position.x + platformWidth, RightPlatform.transform.position.y));
        LeftPlatform.GetComponent<Platform>().Deactivate();
        LeftPlatform = CurrentPlatform;
        CurrentPlatform = RightPlatform;
        RightPlatform = platform;
    }

    private void ActivateLeftPlatform()
    {
        Platform platform = ObjectPool.Instance.GetPlatform().GetComponent<Platform>();
        platform.ActivateAndSetPosition(new Vector2(LeftPlatform.transform.position.x - platformWidth, LeftPlatform.transform.position.y));
        RightPlatform.GetComponent<Platform>().Deactivate();
        RightPlatform = CurrentPlatform;
        CurrentPlatform = LeftPlatform;
        LeftPlatform = platform;
    }
}