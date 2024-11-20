using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace c03.exercise
{
    public class ShootingTower : MonoBehaviour
    {
        //Define here all the variables you will be needing in the script and also all the objects you with to set a reference via the inspector
        public GameObject bullet;
        public GameObject target;
        private Transform bulletOrigin;
        [SerializeField] private int viewDistance = 10;
        [SerializeField] private float viewAngle = 45f;
        [SerializeField] private LayerMask obstacleLayer;
        [SerializeField] private float rotationSpeed = 60f;
        [SerializeField] private float repeatRate = 1f;
        [SerializeField] private float bulletSpeed = 1000f;
        void Start()
        {
            //Retrieve here all the references to other object you will need in the script.

        }

        void Update()
        {
            // Constantly Rotate Tower if Target is NOT in Sight
            //You can use the function transform.Rotate(): https://docs.unity3d.com/ScriptReference/Transform.Rotate.html
            if (!IsTargetVisible(/* Some varialbes...*/))
            {
                transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
                CancelInvoke("Shoot");

            }



            //Check if Target is visible to the tower
            if (IsTargetVisible())
            {
                PointTarget();

                //Start Shooting, if already started Shooting don't invoke again
                //Have a peek to the way we manage an automatic repeated function call in the ShootingTower.cs script. 

                //As a suggestion, we use the function InvokeRepeating(): https://docs.unity3d.com/ScriptReference/MonoBehaviour.InvokeRepeating.html
                if (!IsInvoking("Shoot"))
                {
                    InvokeRepeating("Shoot", 0, repeatRate);
                }




            }

        }

        private void Shoot()
        {

            Vector3 bulletOrigin = GameObject.Find("RayOrigin").transform.position;
            GameObject bulletIstance=Instantiate(bullet, bulletOrigin, bullet.transform.rotation);
            Vector3 direction = target.transform.position - transform.position;
            bulletIstance.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed);

            Destroy(bulletIstance, 3f); // Destroy bullet after 3 seconds

        }

        private bool IsTargetVisible(/* Some varialbes...*/)
        {
            //In this function you need to check if the target is visible to the tower. This is achieved by checking the three below conditions

            //CHECK IF IS WITHIN VIEW DISTANCE

            if (Vector3.Angle(transform.forward, target.transform.position - transform.position) > viewAngle)
            {
                return false;
            }
            if (Physics.Raycast(transform.position, target.transform.position - transform.position, obstacleLayer, viewDistance))
            {
                Debug.Log("Obstacle in the way");
                return false;
            }
            //CHECK IF FALLS WITHIN VIEW ANGLE

            //CHECK IF THERE ARE NO OBSTACLES

            return true;
        }

        private void PointTarget(/* Some varialbes...*/)
        {
            //Rotate the tower in order to always face the target
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        }
    }
}

