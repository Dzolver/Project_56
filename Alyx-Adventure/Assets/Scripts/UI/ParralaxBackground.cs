using System.Collections;
using UnityEngine;
namespace AlyxAdventure
{

    public class ParralaxBackground : MonoBehaviour
    {
        //public GameObject CurrentSprite;
        //public GameObject LeftSprite;
        //public GameObject RightSprite;

        public float scrollSpeedFactor;

        //private Vector3 startPosition;
        //private Renderer myRenderer;
        private Direction direction;
        Renderer mRenderer;

        private void Awake()
        {
            mRenderer = GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            MyEventManager.Instance.ChangeMoveDirection.AddListener(ChangeMoveDirection);
        }

        private void OnDisable()
        {
            if (MyEventManager.Instance != null)
            {
                MyEventManager.Instance.ChangeMoveDirection.RemoveListener(ChangeMoveDirection);
            }
        }

        void Start()
        {
            mRenderer.material.mainTextureOffset = Vector2.zero;
            StartCoroutine(ChangeOffset());
            //LeftSprite.GetComponent<Renderer>().material.mainTextureOffset = Vector2.zero;
            //RightSprite.GetComponent<Renderer>().material.mainTextureOffset = Vector2.zero;
        }

        private IEnumerator ChangeOffset()
        {
            while (true)
            {
                Debug.Log("changing - " + name);
                mRenderer.material.mainTextureOffset = mRenderer.material.mainTextureOffset + new Vector2((int)direction * scrollSpeedFactor, 0f);
                yield return 0.1f;
            }
        }

        //void Update()
        //{
        //    if (direction == Direction.Right)
        //    {
        //        transform.Translate(Vector3.left * Time.deltaTime * (Mathf.Abs(GameData.Instance.RunnerVelocity.x) / scrollSpeedFactor));
        //        if (Math.Truncate(transform.localPosition.x) <= -14f)
        //        {
        //            AddBgToRight();
        //        }
        //    }
        //    else
        //    {
               
        //        transform.Translate(Vector3.right * Time.deltaTime * (Mathf.Abs(GameData.Instance.RunnerVelocity.x) / scrollSpeedFactor));
        //        if (Math.Truncate(transform.localPosition.x) >= 14f)
        //        {
        //            AddBgToLeft();
        //        }
        //    }


        //}

        //void AddBgToLeft()
        //{
        //    //Debug.Log("Adding to left");
        //    GameObject temp = RightSprite;
        //    RightSprite = CurrentSprite;
        //    CurrentSprite = LeftSprite;
        //    LeftSprite = temp;
        //    LeftSprite.transform.localPosition = new Vector3(CurrentSprite.transform.localPosition.x - 1, 0, 0);
        //    transform.localPosition = new Vector3(4.5f, transform.position.y, transform.position.z);
        //}

        //void AddBgToRight()
        //{
        //    //Debug.Log("Adding to right");
        //    GameObject temp = LeftSprite;
        //    LeftSprite = CurrentSprite;
        //    CurrentSprite = RightSprite;
        //    RightSprite = temp;
        //    RightSprite.transform.localPosition = new Vector3(CurrentSprite.transform.localPosition.x + 1, 0, 0);
        //    transform.localPosition = new Vector3(4.5f, transform.position.y, transform.position.z);
        //}

        private void ChangeMoveDirection(Direction direction)
        {
            this.direction = direction;
        }

    }
}