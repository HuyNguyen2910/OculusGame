using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Camera eventCam;
    public Transform player;
    public Transform containPoint;
    public AudioSource shootedAudio;
    public AudioSource loseAudio;
    public bool isPlay;
    public float gameTime;
    public float time;

    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI titleText1;
    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private GameObject startObj;
    [SerializeField] private GameObject containTarget;
    [SerializeField] private List<Target> targets;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private string loseString = "Time Over!";
    [SerializeField] private string restartString = "Restart";
    [SerializeField] private string timerString = "Timer: ";
    
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        isPlay = false;
        startButton.onClick.AddListener(StartGame);
    }
    private void Update()
    {
        if (isPlay)
        {
            time -= Time.deltaTime;
            timerText.text = timerString + Mathf.Round(time);
            if (time <= 0)
            {
                TimeOver();
            }
        }
    }
    public void StartGame()
    {
        time = gameTime;
        startObj.SetActive(false);
        containTarget.SetActive(true);
        foreach(Target target in targets)
        {
            //target.ChangePos();
        }

        isPlay = true;
    }
    public void TimeOver()
    {
        isPlay = false;
        timerText.text = timerString + 0;
        loseAudio.Play();
        startObj.gameObject.SetActive(true);
        containTarget.SetActive(false);
        foreach(Transform child in containPoint)
        {
            Destroy(child.gameObject);
        }
        startButton.onClick.AddListener(CanvasScore.Instance.RestartGame);
        startText.text = restartString;
        titleText.text = loseString;
        titleText1.text = loseString;
    }
}
