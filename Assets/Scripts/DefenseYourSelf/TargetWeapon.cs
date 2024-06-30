using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TargetWeapon : MonoBehaviour
{
    [SerializeField] private Transform player;
    private string shieldTag = "Shield";
    private string playerTag = "Player";

    private float reachTime = 8;
    private void Start()
    {
        player = DYSManager.Instance.player;
        transform.DOMove(player.position, reachTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == shieldTag)
        {
            Debug.Log("Block!");
            Destroy(transform.gameObject);
        }
        else if (other.tag == playerTag)
        {
            Debug.Log("Killed!");
            Time.timeScale = 0;
            LoseScreen.Instance.RestartNewGame();
        }
    }
}
