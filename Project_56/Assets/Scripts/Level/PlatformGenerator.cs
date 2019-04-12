using Project56;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform startPoint;
    public float CurrentPlatformWidth, PlatformWidth;
    [SerializeField]
    private Platform CurrentPlatform, LeftPlatform, RightPlatform;

    private void Start()
    {

        CurrentPlatform = ObjectPool.Instance.GetPlatform(1).GetComponent<Platform>();
        CurrentPlatformWidth = CurrentPlatform.GetComponent<BoxCollider2D>().size.x;
        CurrentPlatform.ActivateAndSetPosition(startPoint.localPosition);

        LeftPlatform = GetPlatform();
        PlatformWidth = LeftPlatform.GetComponent<BoxCollider2D>().size.x;
        LeftPlatform.ActivateAndSetPosition(new Vector2(startPoint.position.x - PlatformWidth, startPoint.position.y));

        RightPlatform = GetPlatform();
        RightPlatform.ActivateAndSetPosition(new Vector2(startPoint.position.x + CurrentPlatformWidth, startPoint.position.y));

    }

    private void FixedUpdate()
    {
        if (GameData.Instance.theRunnerTransform.position.x - CurrentPlatform.transform.position.x > CurrentPlatformWidth / 2)
            ActivateRightPlatform();
        else if (CurrentPlatform.transform.position.x - GameData.Instance.theRunnerTransform.position.x > CurrentPlatformWidth / 2)
            ActivateLeftPlatform();

    }

    private Platform GetPlatform()
    {
        return ObjectPool.Instance.GetPlatform(Random.Range(1, 5)).GetComponent<Platform>();
    }

    private void ActivateRightPlatform()
    {
        PlatformWidth = RightPlatform.GetComponent<BoxCollider2D>().size.x;
        Platform platform = GetPlatform();
        platform.ActivateAndSetPosition(new Vector2(RightPlatform.transform.position.x + PlatformWidth, startPoint.position.y));
        LeftPlatform.GetComponent<Platform>().Deactivate();
        LeftPlatform = CurrentPlatform;
        CurrentPlatform = RightPlatform;
        RightPlatform = platform;
        CurrentPlatformWidth = PlatformWidth;
    }

    private void ActivateLeftPlatform()
    {
        Platform platform = GetPlatform();
        PlatformWidth = platform.GetComponent<BoxCollider2D>().size.x;
        platform.ActivateAndSetPosition(new Vector2(LeftPlatform.transform.position.x - PlatformWidth, startPoint.position.y));
        RightPlatform.GetComponent<Platform>().Deactivate();
        RightPlatform = CurrentPlatform;
        CurrentPlatform = LeftPlatform;
        LeftPlatform = platform;
        CurrentPlatformWidth = CurrentPlatform.gameObject.GetComponent<BoxCollider2D>().size.x;
    }
}