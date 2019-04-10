using Project56;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform startPoint;
    public float CurrentPlatformWidth, smallerWidth;
    [SerializeField]
    private Platform CurrentPlatform, LeftPlatform, RightPlatform;

    private void Start()
    {

        CurrentPlatform = ObjectPool.Instance.GetPlatform(1).GetComponent<Platform>(); ;
        CurrentPlatform.ActivateAndSetPosition(startPoint.localPosition);

        LeftPlatform = GetPlatform();
        SetSmallerWidth(LeftPlatform, CurrentPlatform);
        LeftPlatform.ActivateAndSetPosition(new Vector2(startPoint.position.x - smallerWidth, startPoint.position.y));

        RightPlatform = GetPlatform();
        SetSmallerWidth(RightPlatform, CurrentPlatform);
        RightPlatform.ActivateAndSetPosition(new Vector2(startPoint.position.x + smallerWidth, startPoint.position.y));

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
        return ObjectPool.Instance.GetPlatform(Random.Range(1, 4)).GetComponent<Platform>();
    }

    private void SetSmallerWidth(Platform platform1, Platform platform2)
    {
        if (platform1.GetComponent<BoxCollider2D>().size.x < platform2.GetComponent<BoxCollider2D>().size.x)
        {
            smallerWidth = platform1.GetComponent<BoxCollider2D>().size.x;
        }
        else
        {
            smallerWidth = platform2.GetComponent<BoxCollider2D>().size.x;
        }
    }

    private void ActivateRightPlatform()
    {
        Platform platform = GetPlatform();
        SetSmallerWidth(platform, RightPlatform);
        platform.ActivateAndSetPosition(new Vector2(RightPlatform.transform.position.x + smallerWidth, startPoint.position.y));
        LeftPlatform.GetComponent<Platform>().Deactivate();
        LeftPlatform = CurrentPlatform;
        CurrentPlatform = RightPlatform;
        RightPlatform = platform;
        CurrentPlatformWidth = CurrentPlatform.gameObject.GetComponent<BoxCollider2D>().size.x;
    }

    private void ActivateLeftPlatform()
    {
        Platform platform = GetPlatform();
        SetSmallerWidth(platform, LeftPlatform);
        platform.ActivateAndSetPosition(new Vector2(LeftPlatform.transform.position.x - smallerWidth, startPoint.position.y));
        RightPlatform.GetComponent<Platform>().Deactivate();
        RightPlatform = CurrentPlatform;
        CurrentPlatform = LeftPlatform;
        LeftPlatform = platform;
        CurrentPlatformWidth = CurrentPlatform.gameObject.GetComponent<BoxCollider2D>().size.x;
    }
}