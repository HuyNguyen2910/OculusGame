using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DYSManager : MonoBehaviour
{
    public static DYSManager Instance;
    public Transform player;

    [SerializeField] private Button startButton;
    [SerializeField] private GameObject startObj;
    [SerializeField] private List<GameObject> target;
    [SerializeField] private Transform containTarget;
    [SerializeField] private float spawnTime = 2f;
    [SerializeField] private float minSpawnDistance = 5;
    [SerializeField] private float maxSpawnDistance = 20;
    
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Time.timeScale = 1;
        startButton.onClick.AddListener(StartGame);
    }
    private void StartGame()
    {
        startObj.gameObject.SetActive(false);
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

        StartCoroutine(WaitToSpawn());
    }
    private IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(spawnTime);

        SpawnTarget();
    }
}
