using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private float verticalInput;
    private GameObject focalPoint;
    public bool hasPowerUp = false;
    public GameObject powerUpIndicator;
    void Start()
    {
        focalPoint = GameObject.Find("Focal Point");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        
        rb.AddForce(focalPoint.transform.forward* speed * verticalInput);
        powerUpIndicator.transform.position = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            StartCoroutine(PowerUpCountDown());
            powerUpIndicator.SetActive(true);
        }
    }
    IEnumerator PowerUpCountDown()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp=false;
        powerUpIndicator.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 throwDirection = collision.gameObject.transform.position-transform.position;
            enemyRB.AddForce(throwDirection * 10f, ForceMode.Impulse);
            Debug.Log("collided with"+ collision.gameObject.name);
        }
    }
}
