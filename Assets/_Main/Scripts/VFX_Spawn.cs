using DG.Tweening;
using UnityEngine;

public class VFX_Spawn : Singleton<VFX_Spawn>
{
    public GameObject vfx;

    Tween tween;

    public void Play(Vector3 pos)
    {
        if (tween != null)
        {
            tween.Kill();
            tween = null;
        }
        vfx.transform.localScale = Vector3.zero;
        transform.position = pos;
        vfx.SetActive(true);
        tween = vfx.transform.DOScale(Vector3.one, .2f);
        tween.onComplete += () =>
        {
            vfx.gameObject.SetActive(false);
        };
    }
}
