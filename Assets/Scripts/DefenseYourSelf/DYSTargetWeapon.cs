using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DYSTargetWeapon : MonoBehaviour
{
    public int soundDestroy;

    [SerializeField] private Transform player;

    private string shieldTag = "Shield";
    private string playerTag = "Player";

    private float reachTime = 8;
    private void Start()
    {
        player = GameManager.Instance.player;
        transform.DOMove(player.position, reachTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == shieldTag)
        {
            Debug.Log("Block!");
            DYSCanvas.Instance.SetBlockScore();
            soundDestroy = 1;
            Destroy(transform.gameObject);
        }
        else if (other.tag == playerTag)
        {
            Debug.Log("Killed!");
            Time.timeScale = 0;
            GameManager.Instance.SetButtonStart();
            //DYSCanvas.Instance.Lose();
        }
    }
    private void OnDestroy()
    {
        switch (soundDestroy)
        {
            case 1:
                GameManager.Instance.blockedAudio.Play();
                break;
            case 2:
                GameManager.Instance.shootedAudio.Play();
                break;
        }
    }
}
