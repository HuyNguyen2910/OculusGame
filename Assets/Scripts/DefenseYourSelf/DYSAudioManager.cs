using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DYSAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource shootAudioSource;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Fire();
        }
    }
    public void Fire()
    {
        shootAudioSource.Play();
    }
}
