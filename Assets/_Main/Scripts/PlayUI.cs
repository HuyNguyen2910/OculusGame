using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    public Image fill;
    public Text txtScore, txtTime, txtLive;
    GameCtrl gameCtrl;

    private void Start()
    {
        gameCtrl = GameCtrl.Instance;
        txtScore.text = $"Fruits: {gameCtrl.ScoreCurrent}";

        EventManager.StartListening(EventKey.OnScoreChanged.ToString(), ()=>
        {
            txtScore.text = $"Fruits {gameCtrl.ScoreCurrent}/{gameCtrl.scoreToWin}";
            float process = (float)gameCtrl.ScoreCurrent / gameCtrl.scoreToWin;
            fill.fillAmount = process;
        });
    }
    private void Update()
    {
        float timeLeft = gameCtrl.maxTimePlay - gameCtrl.CountTime;
        txtTime.text = $"{(int)timeLeft}";
        txtLive.text = $"x {gameCtrl.Live}";
    }
}
