using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace c02.colombo.federico
{
    public class RandomPositioning : MonoBehaviour
    {
        //What do you need?
        // A reference to the Character GameObject
        // A variable to store the minimum distance
        public GameObject character;
        public Vector3 randomPos;
        public float minDistance = 10f;
        public float distance;


        void Start()
        {
            
            RandomSpawnPos();

        }

        void Update()
        {
            //Check if the Chracter reference is valid 
            distance = Vector3.Distance(character.transform.position, transform.position);
            if (distance<3)
            {
                RandomSpawnPos();
            }   
            //If it exists check if the distance between the target and the moving character is smaller than a minimum distance
            //To calculate distance use the function Vector3.Distance();
            //Reference: https://docs.unity3d.com/ScriptReference/Vector3.Distance.html

            //If the character has reached the target, reposition it (this game object) in a new random position if you defined a function, this is the place
            //to invoke it
        }
        void RandomSpawnPos()
        {
            
            
            while (Vector3.Distance(character.transform.position, transform.position)<minDistance)
            {
                randomPos = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));

                transform.position = randomPos;
            }
                
            
            
        }

    }
}
