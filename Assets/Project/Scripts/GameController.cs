using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int score;
    [SerializeField] private Text txtScore;

    [SerializeField] public GameObject enemyDeathPrefab;

    [SerializeField] public AudioSource fxGame;
    [SerializeField] private AudioClip fxCollectCarrot;
    [SerializeField] public AudioClip fxHitMonster;

    public void AddScore (int qntToAdd) {
        score += qntToAdd;
        txtScore.text = score.ToString();
        fxGame.PlayOneShot(fxCollectCarrot);
    }
}
