using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private Animator playerAnimator;
  private Rigidbody2D playerRigidbody2D;

  [SerializeField]
  private Transform groundCheck;
  [SerializeField]
  private bool isGround = false;
  [SerializeField]
  private float speed;

  private float touchRun = 0.0f;

  private bool facingRight = true;

  private bool isJumpActive = false;
  private int numbersOfJump = 0;
  private int limitOfJumps = 2;
  [SerializeField]
  private float jumpForce;

  // Start is called before the first frame update
  void Start()
  {
    playerAnimator = GetComponent<Animator>();
    playerRigidbody2D = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    isGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    playerAnimator.SetBool("isGrounded", isGround);

    touchRun = Input.GetAxisRaw("Horizontal");
    SetMovementAnimation();

    if(Input.GetButtonDown("Jump")) {
      isJumpActive = true;
    }
  }

  void FixedUpdate()
  {
    MovePlayer(touchRun);
    if(isJumpActive) JumpPlayer();
  }

  void MovePlayer(float horizontalMovement)
  {
    playerRigidbody2D.velocity = new Vector2(touchRun * speed, playerRigidbody2D.velocity.y);

    if (touchRun > 0 && !facingRight || touchRun < 0 && facingRight)
    {
      FlipPlayerSide();
    }
  }

  void JumpPlayer() {
    if (isGround) {
      playerRigidbody2D.AddForce(new Vector2(0, jumpForce));
      isGround = false;
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
}
