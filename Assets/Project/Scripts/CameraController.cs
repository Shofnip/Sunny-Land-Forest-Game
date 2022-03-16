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
  private float smooth = 0.3f;

  [SerializeField]
  private float cameraLimitUp = 2;
  [SerializeField]
  private float cameraLimitDown = 0;
  [SerializeField]
  private float cameraLimitLeft = 0;
  [SerializeField]
  private float cameraLimitRight = 100;

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
