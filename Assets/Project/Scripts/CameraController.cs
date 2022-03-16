using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField]
  private float offsetX = 2;
  [SerializeField]
  private float offsetY = 1;
  [SerializeField]
  private float smooth = 0.1f;

  [SerializeField]
  private float cameraLimitUp;
  [SerializeField]
  private float cameraLimitDown;
  [SerializeField]
  private float cameraLimitLeft;
  [SerializeField]
  private float cameraLimitRight;

  private Transform playerTransform;
  private float playerPosX;
  private float playerPosY;
  
  void Start()
  {
      playerTransform = FindObjectOfType<PlayerController>().transform;
  }

  void FixedUpdate()
  {
      if (!playerTransform) return;

      playerPosX = Mathf.Clamp(playerTransform.position.x + offsetX, cameraLimitLeft, cameraLimitRight);
      playerPosY = Mathf.Clamp(playerTransform.position.y + offsetY, cameraLimitDown, cameraLimitUp);

      transform.position = Vector3.Lerp(transform.position, new Vector3(playerPosX, playerPosY, transform.position.z), smooth);
  }
}
