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
        CurrentPlatform = ObjectPool.Instance.GetPlatform(PlatformType.VeryEasy).GetComponent<Platform>();
        CurrentPlatform.ActivateAndSetPosition(startPoint.localPosition);
        CurrentPlatformWidth = CurrentPlatform.GetComponent<BoxCollider2D>().size.x - 0.01f;

        LeftPlatform = GetPlatform();
        PlatformWidth = LeftPlatform.GetComponent<BoxCollider2D>().size.x - 0.01f;
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
        PlatformType type;
        if (GameData.Instance.MinutesSinceGame <= 0.5f)
        {
            type = (PlatformType)Random.Range((int)PlatformType.VeryEasy, (int)PlatformType.Easy + 1);
        }
        else if (GameData.Instance.MinutesSinceGame <= 1.5f)
        {
            type = (PlatformType)Random.Range((int)PlatformType.VeryEasy, (int)PlatformType.Average + 1);
        }
        else if (GameData.Instance.MinutesSinceGame <= 3f)
        {
            type = (PlatformType)Random.Range((int)PlatformType.Easy, (int)PlatformType.Hard + 1);
        }
        else
            type = (PlatformType)Random.Range((int)PlatformType.Average, (int)PlatformType.Hard + 1);

        return ObjectPool.Instance.GetPlatform(type).GetComponent<Platform>();
    }

    private void ActivateRightPlatform()
    {
        PlatformWidth = RightPlatform.GetComponent<BoxCollider2D>().size.x - 0.01f;
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
        PlatformWidth = platform.GetComponent<BoxCollider2D>().size.x - 0.01f;
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
        do
        {
            if (GameData.Instance.direction == Direction.Right)
                pos = RightPlatform.GetComponent<Platform>().GetEnemyPoint().position;
            else
                pos = LeftPlatform.GetComponent<Platform>().GetEnemyPoint().position;
        }
        while (Mathf.Abs(pos.x - GameData.Instance.theRunnerTransform.position.x) < 10f);

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
        do
        {
            if (GameData.Instance.direction == Direction.Right)
                pos = RightPlatform.GetComponent<Platform>().GetCoinWavePoint().position;
            else
            {
                pos = LeftPlatform.GetComponent<Platform>().GetCoinWavePoint().position;
                rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        } while (Mathf.Abs(pos.x - GameData.Instance.theRunnerTransform.position.x) < 10f);

        coinwave.ActivateAndSetPosition(pos, rotation);
    }


}