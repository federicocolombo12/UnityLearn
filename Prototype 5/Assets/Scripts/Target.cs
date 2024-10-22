using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    public int downLimit;
    public int upLimit;
    public int rotationValue;
    public float positionX;
    private GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosionParticle;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce (RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(),RandomTorque(), ForceMode.Impulse);
        transform.position = RandomPos();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy (gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
       
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(downLimit, upLimit);
    }
    int RandomTorque()
    {
        return Random.Range(-rotationValue, rotationValue);
    }
    Vector3 RandomPos()
    {
        return new Vector3(Random.Range(-positionX, positionX), -2);
    }
}
