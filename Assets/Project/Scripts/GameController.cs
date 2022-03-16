using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int score;
    [SerializeField]
    private Text txtScore;

    public void AddScore (int qntToAdd) {
        score += qntToAdd;
        txtScore.text = score.ToString();
    }
}