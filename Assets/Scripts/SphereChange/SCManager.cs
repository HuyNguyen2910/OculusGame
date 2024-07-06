using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SCManager : MonoBehaviour
{
    public static SCManager Instance;
    public Target target;
    public List<Transform> spawnPos;
    public Transform containTarget;
    public GameObject startObj;
    public AudioSource shootedAudio;
    public AudioSource loseAudio;
    public Button startButton;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI startText;
    public Vector3 leftVector;
    public Vector3 rightVector;
    public Vector3 centerVector;
    public float minSpawnTime = 0.2f;
    public float maxSpawnTime = 1f;
    public float lastSpawnTime = 0;
    public float spawnTime = 0;

    public bool isPlay;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        isPlay = false;
        startButton.onClick.AddListener(StartGame);
    }
    void Update()
    {
        if (isPlay && Time.time >= lastSpawnTime + spawnTime)
        {
            SpawnTarget();
        }
    }
    void UpdateSpawnTime()
    {
        lastSpawnTime = Time.time;
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
    public void StartGame()
    {
        isPlay = true;
        startObj.SetActive(false);
        foreach(Transform child in containTarget)
        {
            Destroy(child.gameObject);
        }
        CanvasScore.Instance.RestartGame();
        SpawnTarget();
    }
    public void SpawnTarget()
    {
        int index = Random.Range(0, spawnPos.Count - 1);
        Vector3 direction = new Vector3();

        Target tar = Instantiate(target, spawnPos[index].position, Quaternion.identity, containTarget);
        switch (spawnPos[index].GetComponent<Pos>().direction)
        {
            case 0:
                direction = leftVector;
                break;
            case 1:
                direction = rightVector;
                break;
            case 2:
                direction = centerVector;
                break;
        }
        tar.Move(direction);
        UpdateSpawnTime();
    }
    public void Lose()
    {
        isPlay = false;
        loseAudio.Play();
        startObj.gameObject.SetActive(true);
        //startButton.onClick.AddListener(CanvasScore.Instance.RestartGame);
        startText.text = "Restart";
        titleText.text = "You lose!";
    }
}
