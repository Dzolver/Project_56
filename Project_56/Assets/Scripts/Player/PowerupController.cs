using System.Collections;
using UnityEngine;

namespace Project56
{
    public class PowerupController : MonoBehaviour
    {
        private float WaitTime = 10f;
        public GameObject InvincibilityGO;
        public GameObject ScoreMultiplier;
        public GameObject FastRun;

        private void Start()
        {
            StartCoroutine(SpawnPowerups());
        }


        private IEnumerator SpawnPowerups()
        {
            BasePowerup powerup;
            GameObject go = InvincibilityGO;
            while (true)
            {
                yield return new WaitForSeconds(WaitTime);

                int random = Random.Range(1, 4);
                switch (random)
                {
                    case (int)PowerupType.Invincibility:
                        go = InvincibilityGO;
                        break;
                    case (int)PowerupType.ScoreMultiplier:
                        go = ScoreMultiplier;
                        break;
                    case (int)PowerupType.FastRunInvincibility:
                        go = FastRun;
                        break;
                }
                powerup = Instantiate(go).GetComponent<BasePowerup>();
                //Activation logic
                powerup.ActivateAndSetPosition(new Vector2(GameData.Instance.GetNextObjectPosX(), -2.6f));

            }

        }

    }
}