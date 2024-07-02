using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DYSPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource shootAudioSource;
    void Update()
    {
        float f = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
        if (f > 0f || Input.GetKeyDown(KeyCode.P))
        {
            Fire();
        }
    }
    public void Fire()
    {
        shootAudioSource.Play();
    }
}
