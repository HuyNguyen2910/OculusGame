using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DYSCanvas : MonoBehaviour
{
    public static DYSCanvas Instance;

    [SerializeField] private Button RestartButton;
    [SerializeField] private GameObject loseImage;
    [SerializeField] private TextMeshProUGUI blockHSText;
    [SerializeField] private TextMeshProUGUI shootHSText;

    private string highScoreString = "Block : ";

    private string blockHSKey = "BlockHS";
    private string shootHSKey = "ShootHS";
    private string sceneName = "DefenseYourSelf";

    private int blockHS;
    private int shootHS;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        RestartButton.onClick.AddListener(RestartGame);
        GetHighScore();
    }
    public void RestartNewGame()
    {
        loseImage.SetActive(true);
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(sceneName);
        loseImage.SetActive(false);
    }
    private void GetHighScore()
    {
        blockHS = PlayerPrefs.GetInt(blockHSKey);
        //blockHSText.text = levelString + highScore;
    }
}
