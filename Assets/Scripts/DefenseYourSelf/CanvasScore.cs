using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasScore : MonoBehaviour
{
    public static CanvasScore Instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private string scoreString = "Score:\n";

    private int score;
    private void Awake()
    {
        Instance = this;
    }
    public void RestartGame()
    {
        score = 0;
        SetScore();
        GameManager.Instance.StartGame();
    }
    public void AddScore(int addScore)
    {
        score += addScore;
        SetScore();
    }
    private void SetScore()
    {
        scoreText.text = scoreString + score;
    }
}
