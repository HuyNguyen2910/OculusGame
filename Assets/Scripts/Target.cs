using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Target : MonoBehaviour
{
    [SerializeField] private Point point;
    [SerializeField] private float showTime = 5;
    [SerializeField] private float spawnDistance = 5;
    [SerializeField] private MeshRenderer mesh;
    public Sequence sequence;

    private float timer;

    private void Start()
    {
        transform.DORotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)), 20);
        //ChangePos();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explore")
        {
            if (SCManager.Instance.isPlay)
            {
                SCManager.Instance.Lose();
            }
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        //timer += Time.deltaTime;
        //if (timer > showTime)
        //{
        //    ChangePos();
        //}
    }
    //public void ChangePos()
    //{
    //    timer = 0;
    //    transform.position = Random.onUnitSphere * spawnDistance + new Vector3(0, 2, 0);
    //    transform.position = new Vector3(
    //        transform.position.x,
    //        Mathf.Abs(transform.position.y),
    //        transform.position.z);

    //    transform.LookAt(playerCam);
    //}
    //public void SpawnPoint(int s)
    //{
    //    Point pointSpawned = Instantiate(point, GameManager.Instance.containPoint);
    //    pointSpawned.transform.position = transform.position;
    //    pointSpawned.ShowPoint(s);
    //    ChangePos();
    //}
    public void Move(Vector3 direction)
    {
        GetComponent<Rigidbody>().velocity = direction;
        //if (SCManager.Instance.isPlay)
        //{
        //    sequence = DOTween.Sequence();
        //    sequence.Append(transform.DOScale(Vector3.one, 4)).AppendCallback(() => SCManager.Instance.Lose(this));
        //}
    }
    //public void Explore()
    //{
    //    mesh.material.color = Color.red;
    //}
}
