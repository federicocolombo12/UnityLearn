using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace c02.colombo.federico
{
    public class SimpleThirdPersonController : MonoBehaviour
    {
        //Which public variables do you need?
        // A Camera
        public Camera cam;
        // A Rotation Speed
        public float rotationSpeed = 10f;
        // A Movement Speed
        public float movementSpeed = 10f;
        public float horizontalMovement;
        private float verticalMovement;
        private bool isMoving = false;

        void Update()
        {
            
            //Get the Input using Input.GetAxis() & assign the values to a new direction Vector3
            horizontalMovement = Input.GetAxis("Horizontal");
            verticalMovement = Input.GetAxis("Vertical");
            if (horizontalMovement != 0 || verticalMovement != 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            Vector3 direction = new Vector3(horizontalMovement, 0, verticalMovement);

            //Compute direction According to Camera Orientation (use function TransformDirection)
            direction = cam.transform.TransformDirection(direction);
            direction.y = 0.0f;
            direction.Normalize();
            //Calculate the new direction vector between the current forward and the target direction calculated previously.
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0.0f);
            //Rotate the object, you can use Quaternion.LookRotation() function.
            transform.rotation = Quaternion.LookRotation(newDirection);
            //Translate along forward
            if (isMoving)
            {
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            
        }
    }
}
