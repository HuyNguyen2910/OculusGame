using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using StarterAssets;

public class FrontScreen : MonoBehaviour
{
    public static FrontScreen Instance;

    [SerializeField] private CharacterController player;
    [SerializeField] private Button startButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI annouceText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Transform arrow;

    [SerializeField] private List<directionEnum> directionLevel;

    private string levelString = "Level ";
    private string animationArrow = "Arrow";
    private string readyString = "Ready...";
    private string moveString = "Move!";
    private string doneString = "Done!\nNext level!";
    private string prepareString = "Prepare...";
    private string highScoreKey = "HighScore";
    private int countShowArrow;
    private int highScore;
    private float time;
    private float endTime = 1;
    private float waitToRunTime = 2;
    private float waitToShowArrow = 1;
    private float scaleTime = 0.2f;
    private bool isTiming = false;
    private bool isStartGame = false;

    private Vector3 upRotation = new Vector3(0, 0, 0);
    private Vector3 downRotation = new Vector3(0, 0, 180);
    private Vector3 leftRotation = new Vector3(0, 0, 90);
    private Vector3 rightRotation = new Vector3(0, 0, -90);
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //GetHighScore();

        startButton.onClick.AddListener(StartGame);
        continueButton.onClick.AddListener(GameManager.Instance.ContinueLevel);
    }
    private void Update()
    {
        if (!isStartGame && Input.GetKeyDown(KeyCode.P))
        {
            StartGame();
            isStartGame = true;
        }
        if (isTiming)
        {
            time += Time.deltaTime;
            if (time > endTime)
            {
                time = 0;
                isTiming = false;
                CheckDirectionLevel();
            }
        }
    }
    //private void GetHighScore()
    //{
    //    highScore = PlayerPrefs.GetInt(highScoreKey);
    //    highScoreText.text = levelString + highScore;
    //}
    public void StartGame()
    {
        ResetPlayerPosition();
        startButton.gameObject.SetActive(false);
        GameManager.Instance.CreateDirectionLevel();
    }
    private void ResetPlayerPosition()
    {
        player.enabled = false;
        player.transform.position = new Vector3(0, 1, 0);
        player.transform.rotation = Quaternion.Euler(upRotation);
    }
    public void ShowLevel(int level)
    {
        levelText.text = levelString + level;
        if (level > highScore)
        {
            highScoreText.text = levelText.text;
            PlayerPrefs.SetInt(highScoreKey, level);
            PlayerPrefs.Save();
        }
    }
    public void GetDirectionList(List<directionEnum> direction)
    {
        annouceText.transform.DOScale(0, scaleTime);
        directionLevel = direction;
        //CheckDirectionLevel();
        if (countShowArrow < directionLevel.Count)
        {
            UpdateAnnouce(prepareString, () => StartCoroutine(WaitToShowArrow()));
        }
    }
    private IEnumerator WaitToShowArrow()
    {
        yield return new WaitForSeconds(waitToShowArrow);

        OffAnnouce(CheckDirectionLevel);
    }
    private void CheckDirectionLevel()
    {
        if (countShowArrow < directionLevel.Count)
        {
            ShowArrow();
        }
        else
        {
            ReadyRun();
        }
    }
    private void ShowArrow()
    {
        switch (directionLevel[countShowArrow])
        {
            case directionEnum.up:
                arrow.rotation = Quaternion.Euler(upRotation);
                break;
            case directionEnum.down:
                arrow.rotation = Quaternion.Euler(downRotation);
                break;
            case directionEnum.left:
                arrow.rotation = Quaternion.Euler(leftRotation);
                break;
            case directionEnum.right:
                arrow.rotation = Quaternion.Euler(rightRotation);
                break;
        }
        arrow.GetComponent<Animator>().Rebind();
        arrow.GetComponent<Animator>().Play(animationArrow);
        countShowArrow += 1;
        isTiming = true;
    }
    private void ReadyRun()
    {
        countShowArrow = 0;
        annouceText.text = readyString;
        annouceText.transform.DOScale(1, scaleTime);
        StartCoroutine(WaitToRun());
    }
    private IEnumerator WaitToRun()
    {
        yield return new WaitForSeconds(waitToRunTime);
        player.enabled = true;
        UpdateAnnouce(moveString, GameManager.Instance.Run);
    }
    public void EndLevel()
    {
        UpdateAnnouce(doneString, null);
        SetContinueButton(true);
    }
    public void UpdateAnnouce(string str, Action action)
    {
        DOTween.Sequence().Append(annouceText.transform.DOScale(0, scaleTime))
            .AppendCallback(() => annouceText.text = str)
            .Append(annouceText.transform.DOScale(1, scaleTime))
            .AppendCallback(() => action?.Invoke());
    }
    public void OffAnnouce(Action action)
    {
        DOTween.Sequence().Append(annouceText.transform.DOScale(0, scaleTime)).AppendCallback(() => action?.Invoke());
    }
    public void SetContinueButton(bool isEnable)
    {
        continueButton.gameObject.SetActive(isEnable);
    }
}
