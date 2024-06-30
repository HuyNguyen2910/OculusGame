using System;
using System.Collections;
using System.Runtime.InteropServices;
using TigerForge;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCtrl : Singleton<GameCtrl>
{
    [SerializeField] int scoreCurrent = 0;
    public int ScoreCurrent
    {
        set {
            scoreCurrent = value;
            if (scoreCurrent >= scoreToWin)
            {
                Win();
            }
        }
        get => scoreCurrent;
    }


    [SerializeField] float countTime;
    public float CountTime
    {
        set
        {
            countTime = value;
            if (countTime > 60 && countTime < 120)
            {
                if (Level != 2)
                {
                    Level = 2;
                }
            }
            else if (countTime >= 120)
            {
                if (Level != 3)
                {
                    Level = 3;
                }
            }

            if (countTime >= maxTimePlay)
            {
                Lose();
            }
        }
        get { return countTime; }
    }

    public GameObject playUI, standyUI, winUI, loseUI, shopUI;

    public float maxTimePlay = 300;
    public int scoreToWin = 1000;

    [SerializeField] int live;
    public int Live
    {
        set {
            if (value <= 0)
            {
                Lose();
                return;
            }
            int pre = value - live;
            print($"Live increase {pre}");
            live = value;
        }
        get { return live; }
    }


    public bool isPlaying = false;
    public bool isPaused = false;

    [SerializeField] int level = 1;
    public int Level
    {
        set
        {
            level = value;
            switch (level)
            {
                case 1:
                    ObjSpawnMng.Instance.Init(new Vector2(1f, 3f), 4);
                    break;
                case 2:
                    ObjSpawnMng.Instance.Init(new Vector2(.5f, 2), 3);
                    break;
                case 3:
                    ObjSpawnMng.Instance.Init(new Vector2(.5f, 1), 2);
                    break;
            }

        }
        get { return level; }
    }

    CaiGio[] gios;
    private IEnumerator Start()
    {
        gios = FindObjectsOfType<CaiGio>();
        OVRInputRaycast.Instance.IsActive = false;
        EventManager.StartListening(EventKey.OnScoreChanged.ToString(), () =>
        {
            ScoreCurrent = 0;
            for (int i = 0; i < gios.Length; i++)
            {
                ScoreCurrent += gios[i].score;
            }
        });

        standyUI.SetActive(true);
        yield return standyUI.GetComponent<Standy>().StartProcess();

        playUI.SetActive(true);
        isPlaying = true;

        Live = 3;
        Level = 1;

        StartCoroutine(ObjSpawnMng.Instance.StartSpawn());
    }
    private void Update()
    {

        if (isPlaying)
        {
            CountTime += Time.deltaTime;

            if (OVRInputCtrl.ButtonTwoClick())
            {
                OpenShop();
            }
        }
    }
    private void Lose()
    {
        OVRInputRaycast.Instance.IsActive = true;
        isPlaying = false;
        playUI.SetActive(false);
        loseUI.SetActive(true);

        EventManager.EmitEvent(EventKey.OnLose.ToString());
    }
    private void Win()
    {
        OVRInputRaycast.Instance.IsActive = true;
        isPlaying = false;
        playUI.SetActive(false);
        winUI.SetActive(true);
        EventManager.EmitEvent(EventKey.OnWin.ToString());
    }
    public void OpenShop()
    {
        OVRInputRaycast.Instance.IsActive = true;
        shopUI.SetActive(true);

        if (isPlaying)
        {
            isPaused = true;
            playUI.SetActive(false);

        }
        else
        {
            Standy.Instance.isOpenShop = true;
        }
    }
    public void CloseShop()
    {
        shopUI.SetActive(false);
        OVRInputRaycast.Instance.IsActive = false;
        if (isPlaying)
        {
            isPaused = false;
            playUI.SetActive(true);
        }
        else
        {
            Standy.Instance.isCloseShop = true;
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}