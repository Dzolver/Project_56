using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    private float m_MoveSpeed = 2.0f;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> AnimSprites;
    WaitForSeconds endOfFrame = new WaitForSeconds(0.1f);
    [SerializeField]
    private EnemyType enemyType;
    Coroutine routine;
    public virtual void Move(Direction direction)
    {
        
    }

    public virtual void ActivateAndSetPosition(Vector3 position, Transform Parent)
    {
        gameObject.SetActive(true);
        transform.position = position;
        if (GameData.Instance.direction == Direction.Left)
        {
            spriteRenderer.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        transform.SetParent(Parent);
        StartCoroutine(AnimateEnemy());
        routine = StartCoroutine(MoveEnemy(GameData.Instance.direction));
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = Vector3.zero;
        spriteRenderer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        StopCoroutine(routine);
    }

    public float GetMoveSpeed()
    {
        return m_MoveSpeed;
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameStrings.Weapon))
        {
            GameData.Instance.AddKills();
            Deactivate();
        }
        if (collision.gameObject.CompareTag(GameStrings.MainCamera))
        {
            Deactivate();
        }
    }

    public EnemyType GetEnemyType()
    {
        return enemyType;
    }

    IEnumerator AnimateEnemy()
    {
        int i = 0;
        while(true)
        {
            if (i == AnimSprites.Count)
                i = 0;

            spriteRenderer.sprite = AnimSprites[i++];
           
            yield return endOfFrame;
        }
    }

    IEnumerator MoveEnemy(Direction direction)
    {
        while (true)
        {
            Move(direction);
            yield return null;
        }
    }

    public enum EnemyType
    {
        Zombie,
        Raven
    }
}