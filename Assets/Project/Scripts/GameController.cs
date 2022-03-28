using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
  [SerializeField] private Text txtScore;
  [SerializeField] public GameObject enemyDeathPrefab;
  [SerializeField] private AudioSource fxGame;
  [SerializeField] private AudioClip fxCollectCarrot;
  [SerializeField] private AudioClip fxHitMonster;
  [SerializeField] private AudioClip fxDie;
  [SerializeField] private Image lifebar;
  [SerializeField] private Sprite[] lifebarImages;

  private int score;

  public AudioSource FxGame { get => fxGame; }
  public AudioClip FxHitMonster { get => fxHitMonster; }
  public AudioClip FxDie { get => fxDie; }

  public void AddScore(int qntToAdd)
  {
    score += qntToAdd;
    txtScore.text = score.ToString();
    fxGame.PlayOneShot(fxCollectCarrot);
  }

  public void ChangeLifebar(int playerLife)
  {
    lifebar.sprite = lifebarImages[playerLife];
  }
}
