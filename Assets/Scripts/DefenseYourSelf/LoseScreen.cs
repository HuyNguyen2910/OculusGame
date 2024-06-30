using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public static LoseScreen Instance;

    [SerializeField] private Button RestartButton;
    [SerializeField] private GameObject loseImage;

    private string sceneName = "DefenseYourSelf";
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        RestartButton.onClick.AddListener(RestartGame);
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
}
