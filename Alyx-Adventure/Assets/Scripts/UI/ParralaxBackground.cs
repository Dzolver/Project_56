﻿using System;
using UnityEngine;
namespace AlyxAdventure
{

    public class ParralaxBackground : MonoBehaviour
    {
        public Camera MyCamera;
        public GameObject CurrentSprite;
        public GameObject LeftSprite;
        public GameObject RightSprite;

        public float scrollSpeedFactor;
      
        //private Vector3 startPosition;
        //private Renderer myRenderer;
        private Direction direction;


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
            CurrentSprite.GetComponent<Renderer>().material.mainTextureOffset = Vector2.zero;
            LeftSprite.GetComponent<Renderer>().material.mainTextureOffset = Vector2.zero;
            RightSprite.GetComponent<Renderer>().material.mainTextureOffset = Vector2.zero;
        }

        void Update()
        {
            if (direction == Direction.Right)
            {
                transform.Translate(Vector3.left * Time.deltaTime * (Mathf.Abs(GameData.Instance.RunnerVelocity.x)/scrollSpeedFactor));
                if (Math.Truncate(CurrentSprite.transform.position.x) == -10f)
                {
                    AddBgToRight();
                }
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * (Mathf.Abs(GameData.Instance.RunnerVelocity.x) / scrollSpeedFactor));
                if (Math.Truncate(CurrentSprite.transform.position.x) == 10f)
                {
                    AddBgToLeft();
                }
            }
           
           
        }

        void AddBgToLeft()
        {
            //Debug.Log("Adding to left");
            GameObject temp = RightSprite;
            RightSprite = CurrentSprite;
            CurrentSprite = LeftSprite;
            LeftSprite = temp;
            LeftSprite.transform.localPosition = new Vector3(CurrentSprite.transform.localPosition.x - 1, 0, 0);

        }

        void AddBgToRight()
        {
            //Debug.Log("Adding to right");
            GameObject temp = LeftSprite;
            LeftSprite = CurrentSprite;
            CurrentSprite = RightSprite;
            RightSprite = temp;
            RightSprite.transform.localPosition = new Vector3(CurrentSprite.transform.localPosition.x + 1, 0, 0);
        }

        private void ChangeMoveDirection(Direction direction)
        {
            this.direction = direction;
        }

    }
}