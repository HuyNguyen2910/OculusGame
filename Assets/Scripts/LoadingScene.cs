using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public static LoadingScene Instance;

    [SerializeField] public GameObject loseImage;
    private string playSceneName = "PlayScene";
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    public void ReStartNewGame()
    {
        loseImage.SetActive(true);
        StartCoroutine(RestartGame());
    }
    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(playSceneName);
        loseImage.SetActive(false);
    }
}
