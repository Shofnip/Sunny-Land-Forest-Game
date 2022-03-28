using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
  [SerializeField] private Transform platform, pointA, pointB;
  [SerializeField] private float platformVelocity;

  private Vector3 platformDestiny;

  void Start() {
    platform.position = pointA.position;
    platformDestiny = pointB.position;
  }

  void Update()
  {
    if (platform.position == pointA.position) {
      platformDestiny = pointB.position;
    } else if (platform.position == pointB.position) {
      platformDestiny = pointA.position;
    }

    MovePlatform();
  }

  void MovePlatform()
  {
    platform.position = Vector3.MoveTowards(platform.position, platformDestiny, platformVelocity);
  }
}
