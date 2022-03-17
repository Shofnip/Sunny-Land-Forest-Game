using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IASlug : MonoBehaviour
{
  [SerializeField] private Transform enemy;
  private SpriteRenderer enemySprite;

  [SerializeField] private Transform[] waypoints;

  [SerializeField] private float speed;
  private bool isRight = true;

  private int idTarget = 1;

  void Start()
  {
    enemySprite = enemy.gameObject.GetComponent<SpriteRenderer>();
    enemy.position = waypoints[0].position;
  }

  void FixedUpdate()
  {
    if (!enemy) return;

    enemy.position = Vector3.MoveTowards(enemy.position, waypoints[idTarget].position, speed * Time.deltaTime);

    if (enemy.position == waypoints[idTarget].position) {
      idTarget++;
      if(idTarget >= waypoints.Length) {
        idTarget = 0;
      }
    }

    float targetPositionX = waypoints[idTarget].position.x;

    if (targetPositionX < enemy.position.x && isRight == true || targetPositionX > enemy.position.x && isRight == false) {
      Flip();
    }
  }

  void Flip() {
    isRight = !isRight;
    enemySprite.flipX = !enemySprite.flipX;
  }
}
