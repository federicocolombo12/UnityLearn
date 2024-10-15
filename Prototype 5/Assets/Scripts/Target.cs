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
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce (RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(),RandomTorque(), ForceMode.Impulse);
        transform.position = RandomPos();
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy (gameObject);
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
