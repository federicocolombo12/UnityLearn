using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;  // Reference to the player
    public float speed;        // Speed at which the camera will follow the player
    private Transform playerTransf;

    void Start()
    {
        // Get the player's transform component
        playerTransf = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current position of the camera
        Vector3 cameraPosition = transform.position;

        // Follow the player's x position
        cameraPosition.x = Mathf.Lerp(cameraPosition.x, playerTransf.position.x, speed * Time.deltaTime);

        // Update the camera's position
        transform.position = cameraPosition;
    }
}
