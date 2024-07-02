using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DYSManager : MonoBehaviour
{
    public static DYSManager Instance;
    public Transform player;
    public AudioSource shootedAudio;
    public AudioSource blockedAudio;
    public AudioSource loseAudio;
    public float time;

    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private GameObject startObj;
    [SerializeField] private List<GameObject> target;
    [SerializeField] private Transform containTarget;
    [SerializeField] private float spawnTime = 4f;
    [SerializeField] private float minSpawnDistance = 5;
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
    private void SpawnTarget()
    {
        GameObject targetObj = target[Random.Range(0, target.Count - 1)];
        targetObj.transform.position = Random.onUnitSphere * maxSpawnDistance;
        targetObj.transform.position = new Vector3(
            targetObj.transform.position.x, 
            Mathf.Abs(targetObj.transform.position.y), 
            targetObj.transform.position.z);

        targetObj.transform.LookAt(containTarget);
        Instantiate(targetObj, containTarget);
        time = 0.001f;
        //StartCoroutine(WaitToSpawn());
    }
    //private IEnumerator WaitToSpawn()
    //{
    //    yield return new WaitForSeconds(spawnTime);

    //    SpawnTarget();
    //}
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
