using Project56;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform startPoint;
    public float CurrentPlatformWidth, PlatformWidth;
    [SerializeField]
    private Platform CurrentPlatform, LeftPlatform, RightPlatform;

    private void OnEnable()
    {
        MyEventManager.Instance.OnEnemyGenerated.AddListener(OnEnemyGenerated);
        MyEventManager.Instance.OnPowerupGenerated.AddListener(OnPowerupGenerated);
        MyEventManager.Instance.OnCoinWaveGenerated.AddListener(OnCoinWaveGenerated);

    }

    private void OnDisable()
    {
        if (MyEventManager.Instance != null)
        {
            MyEventManager.Instance.OnEnemyGenerated.RemoveListener(OnEnemyGenerated);
            MyEventManager.Instance.OnPowerupGenerated.RemoveListener(OnPowerupGenerated);
            MyEventManager.Instance.OnCoinWaveGenerated.RemoveListener(OnCoinWaveGenerated);
        }
    }

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
        if (GameData.Instance.theRunnerTransform.position.x - CurrentPlatform.transform.position.x > CurrentPlatformWidth)
            ActivateRightPlatform();
        else if (CurrentPlatform.transform.position.x - GameData.Instance.theRunnerTransform.position.x > 0)
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

    private void OnEnemyGenerated(IZombie zombie)
    {
        Vector2 pos;
        if (GameData.Instance.direction == Direction.Right)
            pos = RightPlatform.GetComponent<Platform>().GetEnemyPoint().position;
        else
            pos = LeftPlatform.GetComponent<Platform>().GetEnemyPoint().position;
        zombie.ActivateAndSetPosition(pos);
    }

    private void OnPowerupGenerated(BasePowerup powerup)
    {
        Vector2 pos;
        if (GameData.Instance.direction == Direction.Right)
            pos = RightPlatform.GetComponent<Platform>().GetPowerupPoint().position;
        else
            pos = LeftPlatform.GetComponent<Platform>().GetPowerupPoint().position;
        powerup.ActivateAndSetPosition(pos);
    }


    private void OnCoinWaveGenerated(CoinWave coinwave)
    {
        Vector2 pos;
        Quaternion rotation = Quaternion.identity;
        if (GameData.Instance.direction == Direction.Right)
            pos = RightPlatform.GetComponent<Platform>().GetCoinWavePoint().position;
        else
        {
            pos = LeftPlatform.GetComponent<Platform>().GetCoinWavePoint().position;
            rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        coinwave.ActivateAndSetPosition(pos, rotation);
    }


}