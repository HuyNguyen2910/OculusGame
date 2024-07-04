using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform playerCam;
    [SerializeField] private float showTime = 5;
    [SerializeField] private float spawnDistance = 5;

    private float timer;

    private void Start()
    {
        ChangePos();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > showTime)
        {
            ChangePos();
        }
    }
    public void ChangePos()
    {
        timer = 0;
        transform.position = Random.onUnitSphere * spawnDistance;
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Abs(transform.position.y),
            transform.position.z);

        transform.LookAt(playerCam);
    }
}
