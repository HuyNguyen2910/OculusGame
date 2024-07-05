using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class ButtonOVR : MonoBehaviour
{
    public Button btn;
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
            if (SCManager.Instance.isPlay)
            {
                Debug.Log("Shoot!");
                CanvasScore.Instance.AddScore(1);
                SCManager.Instance.shootedAudio.Play();
                //target.SpawnPoint(score);
                GetComponent<Target>().sequence.Kill();
                SCManager.Instance.SpawnTarget();
                Destroy(gameObject);
            }
        }
    }
}
