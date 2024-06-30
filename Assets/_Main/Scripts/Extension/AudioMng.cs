using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMng : Singleton<AudioMng>
{
    public GameObject hurt;
    public List<AudioSource> sources;

    public void Play(int index)
    {
        sources[index].Play();
    }
}
