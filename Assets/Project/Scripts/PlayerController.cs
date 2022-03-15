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

  // Start is called before the first frame update
  void Start()
  {
    playerAnimator = GetComponent<Animator>();
    playerRigidbody2D = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    touchRun = Input.GetAxisRaw("Horizontal");
  }

  void FixedUpdate()
  {
    MovePlayer(touchRun);
  }

  void MovePlayer(float horizontalMovement) {
    playerRigidbody2D.velocity = new Vector2(touchRun * speed, playerRigidbody2D.velocity.y);

    if (touchRun > 0 && !facingRight || touchRun < 0 && facingRight) {
      FlipPlayerSide();
    }
  }

  void FlipPlayerSide () {
    facingRight = !facingRight;
    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
  }
}
