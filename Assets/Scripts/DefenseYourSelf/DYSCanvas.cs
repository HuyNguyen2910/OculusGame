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
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText1;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreText1;

    [SerializeField] private string blockString = "Block : ";
    [SerializeField] private string shootString = "  Shoot : ";

    private string blockKey = "BlockHS";
    private string shootKey = "ShootHS";
    private string sceneName = "DefenseYourSelf";

    private int blockCount;
    private int shootCount;
    private int blockHS;
    private int shootHS;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        RestartButton.onClick.AddListener(RestartGame);
        GetHighScore();
    }
    //public void Lose()
    //{
    //    loseImage.SetActive(true);
    //}
    public void RestartGame()
    {
        if (blockCount > blockHS)
        {
            blockHS = blockCount;
            PlayerPrefs.SetInt(blockKey, blockHS);
            PlayerPrefs.Save();
            GetHighScore();
        }
        if (shootCount > shootHS)
        {
            shootHS = shootCount;
            PlayerPrefs.SetInt(shootKey, shootHS);
            PlayerPrefs.Save();
            GetHighScore();
        }
        blockCount = 0;
        shootCount = 0;
        SetScore();
        DYSManager.Instance.StartGame();
        //SceneManager.LoadScene(sceneName);
        //loseImage.SetActive(false);
    }
    private void GetHighScore()
    {
        blockHS = PlayerPrefs.GetInt(blockKey);
        shootHS = PlayerPrefs.GetInt(shootKey);
        highScoreText.text = blockString + blockHS + shootString + shootHS;
        highScoreText1.text = highScoreText.text;
    }
    public void SetBlockScore()
    {
        blockCount += 1;
        SetScore();
    }
    public void SetShootScore()
    {
        shootCount += 1;
        SetScore();
    }
    private void SetScore()
    {
        scoreText.text = blockString + blockCount + shootString + shootCount;
        scoreText1.text = scoreText.text;
    }
}
