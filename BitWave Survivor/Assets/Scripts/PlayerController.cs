using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpStrengt;
    private float horizontalSpeed;
    private Rigidbody2D playerRB;
    public bool isOnGround = true;
    private int jumpCount = 0;
    private int slashCount = 0;
    public float slashSpeed = 5;
    void Start()
    {
        playerRB=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround=true;
            jumpCount = 0;
            slashCount = 0;
        }
    }
    void PlayerMovement()
    {
        horizontalSpeed = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalSpeed * Time.deltaTime * speed);
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            isOnGround = false;
            playerRB.AddForce(Vector2.up * jumpStrengt, ForceMode2D.Impulse);
            jumpCount++;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && slashCount < 1)
        {
            playerRB.AddForce(Vector2.right * slashSpeed * horizontalSpeed, ForceMode2D.Impulse);
            slashCount++;

        }
    }
}
