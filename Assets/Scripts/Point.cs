using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private Canvas canvas;
    private float time;
    private void Start()
    {
        canvas.worldCamera = GameManager.Instance.eventCam;
        transform.LookAt(canvas.worldCamera.transform);
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time > 3)
        {
            Destroy(gameObject);
        }
    }
    public void ShowPoint(int point)
    {
        pointText.text = "+" + point;
    }
}
