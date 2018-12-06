using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public class CameraController : MonoBehaviour
    {
        public float speed = 1;//smoothing speed;
        public float edge = 12; //setting how much of an area camera can move around

        private Vector3 lastRunnerPosition;
        private float distanceToMove;
        private float edgeLimit = 30;
        private float speedIncreaseRate;//camera also needs to move faster as player's speed gradually increases
        private float boundaryView = 7.2f;
        private Vector3 targetPosition;

        private void Start()
        {
            //Finding reference to the player
            //theRunner = FindObjectOfType<PlayerController>();
            //Initialize the last player position for the first frame
            lastRunnerPosition = GameData.Instance.theRunnerTransform.position;
            speedIncreaseRate = GameData.Instance.theRunner.GetComponent<PlayerController>().speedIncreaseRate;//setting same increase rate as the player
        }

        private void Update()
        {
            edge += speedIncreaseRate * Time.deltaTime;
            edge = Mathf.Clamp(edge, -edgeLimit, edgeLimit);//making sure edge is clamped
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            //Calculate the distance to move the camera
            distanceToMove = GameData.Instance.theRunnerTransform.position.x + ((int)GameData.Instance.direction * edge) - lastRunnerPosition.x;
            //making sure player doesn't get out of view
            float xPos = Mathf.Clamp(transform.position.x, GameData.Instance.theRunnerTransform.position.x - boundaryView, GameData.Instance.theRunnerTransform.position.x + boundaryView);
            //targetposition to move
            targetPosition = new Vector3(GameData.Instance.theRunnerTransform.position.x + distanceToMove, transform.position.y, transform.position.z);
            //camera's smooth movement
            transform.position = Vector3.Lerp(new Vector3(xPos, transform.position.y, transform.position.z), targetPosition, speed * Time.deltaTime);
            //updating the last player position every frame
            lastRunnerPosition = GameData.Instance.theRunnerTransform.position;
        }

        /*private void OldScript() {
            //Calculate the distance to move the camera
            distanceToMove = theRunner.transform.position.x - lastRunnerPosition.x;

            transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
            //updating the last player position every frame
            lastRunnerPosition = theRunner.transform.position;
        }*/
    }
}