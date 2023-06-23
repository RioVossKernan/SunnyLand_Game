using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    public float horizontalInput;
    public float verticalInput;
    public LayerMask groundLayerMask;
    private bool isGrounded;
    public Transform groundCheck;
    private Rigidbody2D playerRB;
    private BoxCollider2D playerBC;
    private CircleCollider2D playerCC;
    private SpriteRenderer playerSR;
    private Animator playerAnimator;
    private bool isCrouch;
    public bool isDead;
    public Vector3 spawnPoint;
    public int lives = 3;
    private bool isFrozen;
    public TextMeshProUGUI textMeshProUGI;
    public float enemyDeathBounceForce;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerBC = GetComponent<BoxCollider2D>();
        playerCC = GetComponent<CircleCollider2D>();
        playerSR = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //SET VARIABLES
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayerMask);
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //MOVE
        if (!isDead && !isCrouch && !isFrozen)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime * horizontalInput;
        }
        else if (!isDead && isCrouch)
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime * horizontalInput * 0.5f;
        }


        Crouch();
        Jump();
        SpriteFlip();
        SetAnimationParameters();
        Dead();

        if (transform.position.x > 24 && spawnPoint.x < 24)
        {
            spawnPoint = new Vector3(24, -0.5f);
        }
        else if (transform.position.x > 36 && spawnPoint.x < 36)
        {
            spawnPoint = new Vector3(36, -1);
        }
        else if (transform.position.x > 64 && spawnPoint.x < 64)
        {
            spawnPoint = new Vector3(64, -0.5f);
        }
    }

    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && !isFrozen)
        {
            if (isGrounded)
            {
                playerRB.velocity = Vector2.up * jumpForce;
            }
        }
    }

    void SpriteFlip()
    {
        if (horizontalInput > 0)
        {
            playerSR.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            playerSR.flipX = true;
        }
    }

    void SetAnimationParameters()
    {
        playerAnimator.SetBool("Jump", !isGrounded);
        playerAnimator.SetBool("Dead", isDead);
        playerAnimator.SetBool("Crouch", isCrouch);
        playerAnimator.SetBool("Run", horizontalInput != 0);
    }

    void Crouch()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            isCrouch = true;
            playerBC.enabled = false;
        }
        else
        {
            playerBC.enabled = true;
            isCrouch = false;
        }
    }

    int counter;
    void Dead()
    {
        if (isDead)
        {
            Jump();
            counter++;
            playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
            //Respawn
            if (counter > 200)
            {
                playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
                isDead = false;
                lives--;
                transform.position = spawnPoint;
                playerRB.velocity = new Vector3(0, 0, 0);
                counter = 0;
            }
        }
    }


    Collider2D collide;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gem"))
        {
            playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
            isFrozen = true;
            textMeshProUGI.gameObject.SetActive(true);
        }
        else
        {
            collide = collision;
            StartCoroutine("Collision");
        }
    }

    IEnumerator Collision()
    {
        yield return new WaitForEndOfFrame();
        if (collide.CompareTag("DeadlyForPlayer"))
        {
            isDead = true;
        }
    }
    public void bounce()
    {
        playerRB.velocity = new Vector2(0,enemyDeathBounceForce);
    }
}
