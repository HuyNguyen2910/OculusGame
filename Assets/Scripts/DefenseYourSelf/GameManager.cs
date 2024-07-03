using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform player;
    public AudioSource shootedAudio;
    public AudioSource blockedAudio;
    public AudioSource loseAudio;
    public float time;

    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private GameObject startObj;
    [SerializeField] private GameObject target;
    [SerializeField] private Transform containTarget;
    [SerializeField] private float spawnTime = 4f;
    [SerializeField] private float spawnDistance = 5;
    [SerializeField] private float maxSpawnDistance = 20;

    [SerializeField] private string loseString = "YOU LOSE!";
    [SerializeField] private string restartString = "Restart";
    
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }
    private void Update()
    {
        if (time > 0)
        {
            time += Time.deltaTime;
            if (time > spawnTime)
            {
                SpawnTarget();
                time = 0.001f;
            }
        }
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        startObj.gameObject.SetActive(false);
        foreach (Transform transform in containTarget)
        {
            Destroy(transform.gameObject);
        }    
        SpawnTarget();
    }
    public void SpawnTarget()
    {
        target.transform.position = Random.insideUnitCircle * spawnDistance;
        target.transform.position = new Vector3(
            target.transform.position.x, 
            Mathf.Abs(target.transform.position.y), 
            target.transform.position.z);

        target.transform.LookAt(containTarget);
        Instantiate(target, containTarget);
        time = 0.001f;
    }
    public void SetButtonStart()
    {
        loseAudio.Play();
        time = 0;
        startObj.gameObject.SetActive(true);
        startButton.onClick.AddListener(DYSCanvas.Instance.RestartGame);
        startText.text = restartString;
        titleText.text = loseString;
    }
}
