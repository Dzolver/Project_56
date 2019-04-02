using UnityEngine;
namespace Project56
{


    public class ParralaxBackground : MonoBehaviour
    {

        
        public float scrollSpeed;
        private Direction direction;
        Renderer myRenderer;

        private Vector3 startPosition;

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

        private void ChangeMoveDirection(Direction direction)
        {
            this.direction = direction;
        }

        void Start()
        {
            myRenderer = GetComponent<Renderer>();
            myRenderer.material.mainTextureOffset = Vector2.zero;
            startPosition = transform.position;
        }

        void Update()
        {
            if(direction == Direction.Right)
            {
                transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * scrollSpeed);
            }
        }
    }
}