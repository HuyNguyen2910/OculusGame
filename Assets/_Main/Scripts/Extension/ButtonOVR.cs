using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class ButtonOVR : MonoBehaviour
{
    public Button btn;
    public Target target;
    public int score;
    private void Start()
    {
        if (btn != null)
        {
            GetComponent<BoxCollider>().size = GetComponent<RectTransform>().rect.size;
        }
    }
    public void OnRaycastClick()
    {
        if (btn != null)
        {
            //transform.DOScale(new Vector3(.8f, .8f, .8f), .1f).onComplete += () =>
            //{
            //    transform.DOScale(new Vector3(1, 1, 1), .1f).onComplete += () => btn.onClick.Invoke();
            //};
            btn.onClick.Invoke();
        }
        else
        {
            if (GameManager.Instance.isPlay)
            {
                Debug.Log("Shoot!");
                CanvasScore.Instance.AddScore(score);
                GameManager.Instance.shootedAudio.Play();
                target.SpawnPoint(score);
            }
        }
    }
}
