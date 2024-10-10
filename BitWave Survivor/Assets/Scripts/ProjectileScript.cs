using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerScript;
    public float playerDirection;
    public float projectileSpeed;
    public bool isRight;
    private float limitPosition=30;
    
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        playerDirection = playerScript.horizontalSpeed;

    }

    // Update is called once per frame
    void Update()


    {
        CheckDirection();
        
        if (transform.position.x > limitPosition)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < -limitPosition)
        {
            Destroy(gameObject) ;
        }
    }
    void CheckDirection()
    {
        
        if (playerDirection > 0)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }
        if (isRight)
        {
            transform.Translate(Vector2.down * projectileSpeed);
        }
        else
        {
            transform.Translate(Vector2.down * -projectileSpeed);
        }
    }
    
}
