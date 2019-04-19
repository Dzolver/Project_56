using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    private float m_MoveSpeed = 2.0f;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> AnimSprites;
    WaitForSeconds endOfFrame = new WaitForSeconds(0.1f);
    public virtual void Move()
    {
        transform.Translate(Vector3.left * Time.deltaTime * GetMoveSpeed());
    }

    public virtual void ActivateAndSetPosition(Vector3 position, Transform Parent)
    {
        gameObject.SetActive(true);
        transform.position = position;
        if (GameData.Instance.direction == Direction.Left)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        transform.SetParent(Parent);
        StartCoroutine(AnimateEnemy());
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
        gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
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
}