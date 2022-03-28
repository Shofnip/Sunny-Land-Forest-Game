using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

  [SerializeField] private Transform groundCheck;
  [SerializeField] private bool isGround = false;
  [SerializeField] private float speed;
  [SerializeField] private AudioSource fxGame;
  [SerializeField] private AudioClip fxJump;
  [SerializeField] private float jumpForce;
  [SerializeField] private Color noHitColor;
  [SerializeField] private GameObject playerDieObject;

  private float touchRun = 0.0f;
  private bool facingRight = true;
  private bool isJumpActive = false;
  private int numberOfJumps = 0;
  private int maxJumps = 2;
  private Animator playerAnimator;
  private Rigidbody2D playerRigidbody2D;
  private SpriteRenderer playerSpriteRenderer;
  private int life = 3;
  private bool isInvincible = false;

  private GameController gameController;

  void Start()
  {
    playerAnimator = GetComponent<Animator>();
    playerRigidbody2D = GetComponent<Rigidbody2D>();
    playerSpriteRenderer = GetComponent<SpriteRenderer>();
    gameController = FindObjectOfType<GameController>();
  }

  void Update()
  {
    isGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    playerAnimator.SetBool("isGrounded", isGround);

    touchRun = Input.GetAxisRaw("Horizontal");
    SetMovementAnimation();

    if (Input.GetButtonDown("Jump"))
    {
      isJumpActive = true;
    }
  }

  void FixedUpdate()
  {
    MovePlayer(touchRun);
    if (isJumpActive) JumpPlayer();
  }

  void MovePlayer(float horizontalMovement)
  {
    playerRigidbody2D.velocity = new Vector2(touchRun * speed, playerRigidbody2D.velocity.y);

    if (touchRun > 0 && !facingRight || touchRun < 0 && facingRight)
    {
      FlipPlayerSide();
    }
  }

  void JumpPlayer()
  {
    if (isGround)
    {
      numberOfJumps = 0;
    }

    if (isGround || numberOfJumps < maxJumps)
    {
      playerRigidbody2D.AddForce(new Vector2(0, jumpForce));
      isGround = false;
      numberOfJumps++;
      fxGame.PlayOneShot(fxJump);
    }
    isJumpActive = false;
  }

  void FlipPlayerSide()
  {
    facingRight = !facingRight;
    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
  }

  void SetMovementAnimation()
  {
    playerAnimator.SetBool("isWalking", playerRigidbody2D.velocity.x != 0 && isGround);
    playerAnimator.SetBool("isJumping", !isGround);
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    switch (collider.gameObject.tag)
    {
      case "Collectible":
        gameController.AddScore(1);
        Destroy(collider.gameObject);
        break;
      case "Enemy":
        GameObject enemyExplosion = Instantiate(gameController.enemyDeathPrefab, collider.transform.position, collider.transform.localRotation);
        Destroy(enemyExplosion, 0.5f);

        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, 600));

        gameController.FxGame.PlayOneShot(gameController.FxHitMonster);

        Destroy(collider.gameObject);
        break;
      default:
        return;
    }
  }

  void OnCollisionEnter2D(Collision2D collider)
  {
    switch (collider.gameObject.tag)
    {
      case "Enemy":
        Hurt();
        break;
      case "Platform":
        transform.parent = collider.transform;
      break;
    }
  }

  void OnCollisionExit2D (Collision2D collider) {
    switch (collider.gameObject.tag)
    {
      case "Platform":
        transform.parent = null;
      break;
    }
  }

  void Hurt() {
    if (isInvincible) return;

    isInvincible = true;
    life--;
    gameController.ChangeLifebar(life);
    StartCoroutine("Damage");

    if (life < 1) {
      gameObject.SetActive(false);

      GameObject playerDie = Instantiate(playerDieObject, transform.position, Quaternion.identity);
      Rigidbody2D playerDieRB = playerDie.GetComponent<Rigidbody2D>();

      playerDieRB.AddForce(new Vector2(150, 500));
      gameController.FxGame.PlayOneShot(gameController.FxDie);
      Invoke("LoadGame", 4);
    }
  }

  IEnumerator Damage() {
    playerSpriteRenderer.color = noHitColor;
    yield return new WaitForSeconds(0.1f);

    for (float i=0; i<1; i+=0.1f) {
      playerSpriteRenderer.enabled = false;
      yield return new WaitForSeconds(0.1f);
      playerSpriteRenderer.enabled = true;
      yield return new WaitForSeconds(0.1f);
    }

    playerSpriteRenderer.color = Color.white;
    isInvincible = false;
  }

  void LoadGame() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
