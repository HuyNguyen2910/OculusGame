using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TargetWeapon : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float reachTime = 3;
    private void Start()
    {
        transform.DOMove(player.position, reachTime);
    }
}
