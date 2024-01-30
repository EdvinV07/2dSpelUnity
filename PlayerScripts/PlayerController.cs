using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    public Animator anim; // Add a public reference to the Animator component
    public float rollDuration = 1.0f; // Set longer roll duration here
    public float rollSpeed = 10.0f; // Set faster roll speed here
    private static bool isAttacking = false;
    public bool weapons = false;

    public string MainTele;
    public string MiddleGround;
    public string Spawn;
    public UnityEvent onClickEvent;
    public CoinUI coinUI;

    public float throwCooldown = 3f;
    private float cooldownBomb;
    public int loadedLeap = 0;


    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    public int coinCount = 0;
    private bool isBlocking = false;
    public BuyBomb BuyBomb;
    public GameObject PreFabBomb;
    private BuyBomb buyDaBombInstance;


    private bool isRolling = false;
    private float rollTimer = 0.0f;

    private static bool playerSpawned = false;

    [SerializeField] private TextMeshProUGUI Coins;





    private void Start()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Bomb"), true);
        Coins = GameObject.Find("Coins").GetComponent<TextMeshProUGUI>();

        Scene currentScene = SceneManager.GetActiveScene();

        // Reset coin count to 0 when starting the game
        PlayerPrefs.SetInt("CoinCount", 0);
        coinCount = PlayerPrefs.GetInt("CoinCount", 0);


        loadedLeap = PlayerPrefs.GetInt("Leaper", 0);
    }

    void Awake()
    {


        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
       
        bool BombValue = BuyBomb.Bomb;
        cooldownBomb -= Time.deltaTime;


        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Blocking logic
        if (Input.GetKey(KeyCode.Q) && !isRolling && !isAttacking)
        {
            isBlocking = true;
            anim.SetBool("Block", true);
            body.velocity = new Vector2(0, body.velocity.y);
        }
        else
        {
            isBlocking = false;
            anim.SetBool("Block", false);

            if (wallJumpCooldown > 0.2f && !isRolling)
            {
                // Normal movement logic
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            }
        }

        // Jumping logic
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Jump();
            anim.SetTrigger("Jump");
        }

        // Animation control for running and grounded state
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", isGrounded());

        // Roll logic
        if (wallJumpCooldown > 0.2f && !isRolling && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Roll());
        }

        if (wallJumpCooldown > 0.2f)
        {
            if (!isRolling && Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(Roll());
            }

            if (!isRolling)
            {
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            }

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 5;

            if (body.velocity.y < 0)
            {
                anim.SetBool("Falling", true); // Play falling animation
            }
            else
            {
                anim.SetBool("Falling", false);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
                anim.SetTrigger("Jump"); // Play jump animation
            }


  
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

        //Change weapon
        if (Input.GetKeyDown(KeyCode.J))
        {
            weapons = false;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            weapons = true;
        }

        //Attacks
        if (!isRolling && !isAttacking)
        {
            if (horizontalInput != 0)
            {
                anim.SetBool("Idle", false);
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Idle", true);
                anim.SetBool("Run", false);
            }
        }

        // Prioritize attack over run and idle
        if (Input.GetMouseButtonDown(0) && !weapons && !isRolling)
        {
            Attack();
        }

        if (Input.GetMouseButtonDown(0) && weapons && !isRolling)
        {
            Attack2();
        }

        if (Input.GetKey(KeyCode.Q) && cooldownBomb <= 0f)
        {
            // Throw a bomb
            ThrowVoid();

            // Reset the throw cooldown timer
            cooldownBomb = throwCooldown;
        }




    }

    void Attack()
    {
        // Trigger attack animation
        anim.SetTrigger("Attack");
    }

    void Attack2()
    {
        // Trigger attack animation
        anim.SetTrigger("Attack2");
    }

    IEnumerator Roll()
    {
        if (isGrounded() && !isBlocking)
        {
            isRolling = true;
            rollTimer = Time.time;

            anim.SetTrigger("Roll");

            float rollEndTime = Time.time + rollDuration;

            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), true);
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("EnemyHitbox"), true);
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("ForestBoss"), true);
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("HammerMan"), true);

            Vector2 rollDirection = new Vector2(transform.localScale.x, 0).normalized;

            while (Time.time < rollEndTime)
            {
                body.velocity = new Vector2(rollDirection.x * rollSpeed, body.velocity.y + Physics2D.gravity.y * Time.fixedDeltaTime);
                yield return null;
            }

            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), false);
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("EnemyHitbox"), false);
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("ForestBoss"), false);
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("HammerMan"), false);

            isRolling = false;
        }
    }


    public bool IsRolling()
    {
        return isRolling;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x) * transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MainGameTele"))
        {
                Debug.Log("BananBåt");
                SceneManager.LoadScene(MainTele);   
        }

        if (collision.collider.CompareTag("Tele"))
        {
            Debug.Log("BananBåt");
            SceneManager.LoadScene(MiddleGround);
        }

        if (collision.collider.CompareTag("OutOfBounds"))
        {
            SceneManager.LoadScene(Spawn);

        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public void TriggerHitAnimation()
    {
        anim.SetTrigger("Hit");
    }

    public void PickupCoin()
    {
        coinCount++; // Increment the coin count
        PlayerPrefs.SetInt("CoinCount", coinCount);
        UpdateCoinCounter();
       
    }


    private void UpdateCoinCounter()
    {
        // Save the coin count after updating the UI
        PlayerPrefs.SetInt("CoinCount", coinCount);
       
         //Check if coinUI is assigned before updating
        if (coinUI != null)
        {
            Debug.Log("Updating Coin Counter UI");
            coinUI.UpdateCoinCounterUI();
        }
    }



    public void SpawnPos()
    {
        transform.position = new Vector3(0f, 1f, 0f);
       
    }

    private void ThrowVoid()
    {
        GameObject newPrefabInstance = Instantiate(PreFabBomb, transform.position, Quaternion.identity);
    }

    public void LeapAttack()
    {
        Debug.Log("Que");
        anim.SetTrigger("Leap");
    }

    public void LeapPlayer()
    {
        Debug.Log("Yaaas");
        loadedLeap = PlayerPrefs.GetInt("Leaper", 0);
        Debug.Log("Loaded Leaper from PlayerPrefs: " + loadedLeap);
       
    }


}
