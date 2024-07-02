using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DYSPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource shootAudioSource;
    [SerializeField] private bool isShoot = false;
    void Update()
    {
        float f = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
        if (f > 0f && !isShoot && DYSManager.Instance.time != 0)
        {
            isShoot = true;
            Fire();
        }
        else if (f == 0)
            isShoot = false;
    }
    public void Fire()
    {
        shootAudioSource.Play();
    }
}
