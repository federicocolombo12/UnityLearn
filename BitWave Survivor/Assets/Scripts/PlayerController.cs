using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpStrengt;
    public float horizontalSpeed;
    private Rigidbody2D playerRB;
    public bool isOnGround = true;
    private int jumpCount = 0;
    private int slashCount = 0;
    public float slashSpeed = 5;
    public GameObject projectile;
    public bool isPowerupActive = false;
    public GameObject[] enemyArray;
    public bool isDead;
    private SpriteRenderer playerSprite;
    private Animator playerAnim;
    public GameObject gunPrefab;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        isDead = false;
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");// Finds all enemies in the scene
        PlayerMovement();
        PlayerShoot();
        playerAnim.SetBool("onGround", isOnGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpCount = 0;
            slashCount = 0;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("You're Dead!!!"); 
            isDead=true;
        }
    }

    void PlayerMovement()
    {
        if (!isDead)
        {
            horizontalSpeed = Input.GetAxis("Horizontal");
            transform.Translate(Vector2.right * horizontalSpeed * Time.deltaTime * speed);
        }
        
        

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            isOnGround = false;
            playerRB.AddForce(Vector2.up * jumpStrengt, ForceMode2D.Impulse);
            jumpCount++;
            
            
        }

        // Slash/Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && slashCount < 1)
        {
            playerRB.AddForce(Vector2.right * slashSpeed * horizontalSpeed, ForceMode2D.Impulse);
            slashCount++;
        }
        if (horizontalSpeed < 0)
        {
            playerSprite.flipX = true;
        }
        else if (horizontalSpeed > 0)
        {
            playerSprite.flipX = false;
        }
    }

    void PlayerShoot()

    {
        Vector2 spawnLocation = gunPrefab.gameObject.transform.position;
        

        if (Input.GetKeyDown(KeyCode.Mouse0) )
        {
            Instantiate(projectile, spawnLocation, projectile.transform.rotation);
        }
        
    }
    IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(5);
        isPowerupActive = false;
        for (int i = 0; i < enemyArray.Length; i++)
        {
            Enemy enemy = enemyArray[i].GetComponent<Enemy>();
            enemy.enemySpeed *= 2;  // Restore the speed
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            isPowerupActive = true;
            Destroy(collision.gameObject);

            // Reduce speed of all enemies by half
            for (int i = 0; i < enemyArray.Length; i++)
            {
                Enemy enemy = enemyArray[i].GetComponent<Enemy>();  // Store the component reference
                enemy.enemySpeed /= 2;  // Halve the speed of the enemy
            }
            StartCoroutine(PowerUpTimer());
        }
    }
}
