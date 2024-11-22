using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace c03.exercise
{
    public class ShootingTower : MonoBehaviour
    {
        //Define here all the variables you will be needing in the script and also all the objects you with to set a reference via the inspector
        public GameObject bullet;
        public GameObject target;
        public Transform BulletTransform;
        
        [SerializeField] private float viewDistance = 10f;
        [SerializeField] private float viewAngle = 45f;
        [SerializeField] private LayerMask obstacleLayer;
        [SerializeField] private float rotationSpeed = 60f;
        [SerializeField] private float repeatRate = 1f;
        [SerializeField] private float bulletSpeed = 1000f;
        void Start()
        {
            //Retrieve here all the references to other object you will need in the script.

        }

        private void Update()
        {
            // Constantly Rotate Tower if Target is NOT in Sight
            //You can use the function transform.Rotate(): https://docs.unity3d.com/ScriptReference/Transform.Rotate.html
            if (!IsTargetVisible())
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

            Vector3 bulletOrigin = BulletTransform.position;
            GameObject bulletIstance=Instantiate(bullet, bulletOrigin, bullet.transform.rotation);
            Vector3 direction = (target.transform.position - transform.position).normalized;
            bulletIstance.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.Impulse);
            DestroyBullet(bulletIstance);
             // Destroy bullet after 3 seconds

        }

        private bool IsTargetVisible(/* Some varialbes...*/)
        {
            //In this function you need to check if the target is visible to the tower. This is achieved by checking the three below conditions

            //CHECK IF IS WITHIN VIEW DISTANCE
            

            // CHECK IF FALLS WITHIN VIEW ANGLE
            if (Vector3.Angle(transform.forward, target.transform.position - transform.position) > viewAngle)
            {
                Debug.Log("Target is out of view angle");
                return false;


            }
            Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red);
            if (Physics.Raycast(transform.position, target.transform.position - transform.position, 100f,obstacleLayer.value))
            {
                Debug.Log("Hit block");
                return false;
            }
            
            
            return true;
            

// CHECK IF THERE ARE NO OBSTACLES
            

            
        }

        private void PointTarget(/* Some varialbes...*/)
        {
            //Rotate the tower in order to always face the target
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        }

        private void DestroyBullet(GameObject bulletIstance)
        {
            Destroy(bulletIstance, 3f);
            
            
        }
        
    }
    
}

