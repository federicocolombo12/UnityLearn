using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace c02.colombo.federico
{
    public class RandomPositioning : MonoBehaviour
    {
        private GameObject character;
        public Vector3 randomPos;
        public float minDistance = 10f;
        public float distance;
        public int maxAttempts = 100;

        void Start()
        {
            character = GameObject.Find("ThirdPersonMovement");
            RandomSpawnPos();
        }

        void Update()
        {
            if (character != null)
            {
                distance = Vector3.Distance(character.transform.position, transform.position);
                if (distance < 3)
                {
                    RandomSpawnPos();
                }
            }
        }

        void RandomSpawnPos()
        {
            int attempts = 0;
            do
            {
                randomPos = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
                attempts++;
            } while (Vector3.Distance(character.transform.position, randomPos) < minDistance && attempts < maxAttempts);

            if (attempts < maxAttempts)
            {
                transform.position = randomPos;
            }
            else
            {
                Debug.LogWarning("Failed to find a valid position after maximum attempts.");
            }
        }
    }
}