using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project56
{
    public class CameraController : MonoBehaviour
    {
        public PlayerController theRunner;

        private Vector3 lastRunnerPosition;
        private float distanceToMove;

        // Use this for initialization
        private void Start()
        {
            //Finding reference to the player
            theRunner = FindObjectOfType<PlayerController>();
            //Initialize the last player position for the first frame
            lastRunnerPosition = theRunner.transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            //Calculate the distance to move the camera
            distanceToMove = theRunner.transform.position.x - lastRunnerPosition.x;

            transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
            //updating the last player position every frame
            lastRunnerPosition = theRunner.transform.position;
        }
    }
}