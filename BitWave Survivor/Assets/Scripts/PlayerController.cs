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
    private SpriteRenderer powerUpSprite;
    private Animator playerAnim;
    private float projectileSpawn = 1f;
    

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        isDead = false;
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        powerUpSprite = GameObject.Find("SelectionRing_06").GetComponent<SpriteRenderer>();
        powerUpSprite.enabled = false;
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
            playerAnim.SetBool("isDead", true);
        }
    }

    void PlayerMovement()
    {
        if (!isDead)
        {
            horizontalSpeed = Input.GetAxis("Horizontal");
            transform.Translate(Vector2.right * horizontalSpeed * Time.deltaTime * speed);
            if (horizontalSpeed != 0)
            {
                playerAnim.SetFloat("PlayerSpeed", 1);
            }
            else
            {
                playerAnim.SetFloat("PlayerSpeed", 0);
            }
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
        
        

        // Jump
        
    }

    void PlayerShoot()

    {
        Vector2 spawnLocation = transform.position;
        
        if (playerSprite.flipX)
        {
            spawnLocation += new Vector2(-projectileSpawn * 2, 0);
        }
        else if (!playerSprite.flipX)
        {
            spawnLocation += new Vector2(projectileSpawn * 2, 0);
        }
        

        if (Input.GetKeyDown(KeyCode.Mouse0) )
        {
            
            Instantiate(projectile, spawnLocation, projectile.transform.rotation);
            playerAnim.SetBool("isShooting", true);
        }
        else 
    {
        playerAnim.SetBool("isShooting", false);
    }
        
        
    }
    IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(5);
        powerUpSprite.enabled = false;
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
            powerUpSprite.enabled = true;
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
