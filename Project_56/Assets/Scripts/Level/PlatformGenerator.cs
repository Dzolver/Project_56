using Project56;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform startPoint;
    public float CurrentPlatformWidth, LeftPlatformWidth, RightPlatformWidth;
    [SerializeField]
    private Platform CurrentPlatform, LeftPlatform, RightPlatform;

    private void Start()
    {

        CurrentPlatform = ObjectPool.Instance.GetPlatform().GetComponent<Platform>();
        CurrentPlatformWidth = CurrentPlatform.gameObject.GetComponent<BoxCollider2D>().size.x;
        CurrentPlatform.ActivateAndSetPosition(startPoint.localPosition);

        LeftPlatform = ObjectPool.Instance.GetPlatform().GetComponent<Platform>();
        LeftPlatform.ActivateAndSetPosition(new Vector2(startPoint.position.x - CurrentPlatformWidth, startPoint.position.y));

        RightPlatform = ObjectPool.Instance.GetPlatform().GetComponent<Platform>();
        RightPlatform.ActivateAndSetPosition(new Vector2(startPoint.position.x + CurrentPlatformWidth, startPoint.position.y));

    }

    private void FixedUpdate()
    {
        if (GameData.Instance.theRunnerTransform.position.x - CurrentPlatform.transform.position.x > CurrentPlatformWidth / 2)
            ActivateRightPlatform();
        else if (CurrentPlatform.transform.position.x - GameData.Instance.theRunnerTransform.position.x > CurrentPlatformWidth / 2)
            ActivateLeftPlatform();

    }

    private void ActivateRightPlatform()
    {
        RightPlatformWidth = RightPlatform.gameObject.GetComponent<BoxCollider2D>().size.x;
        Platform platform = ObjectPool.Instance.GetPlatform().GetComponent<Platform>();
        platform.ActivateAndSetPosition(new Vector2(RightPlatform.transform.position.x + RightPlatformWidth, startPoint.position.y));
        LeftPlatform.GetComponent<Platform>().Deactivate();
        LeftPlatform = CurrentPlatform;
        CurrentPlatform = RightPlatform;
        RightPlatform = platform;
        CurrentPlatformWidth = RightPlatformWidth;
    }

    private void ActivateLeftPlatform()
    {
        LeftPlatformWidth = LeftPlatform.gameObject.GetComponent<BoxCollider2D>().size.x;
        Platform platform = ObjectPool.Instance.GetPlatform().GetComponent<Platform>();
        platform.ActivateAndSetPosition(new Vector2(LeftPlatform.transform.position.x - LeftPlatformWidth, startPoint.position.y));
        RightPlatform.GetComponent<Platform>().Deactivate();
        RightPlatform = CurrentPlatform;
        CurrentPlatform = LeftPlatform;
        LeftPlatform = platform;
        CurrentPlatformWidth = LeftPlatformWidth;
    }
}