using System.Collections.Generic;
using UnityEngine;

public class PoolingsMng : Singleton<PoolingsMng>   
{
    public List<Pooling> items;
    public Pooling explosionVfx;
    public GameObject GetItem(int indexPooling)
    {
        return items[indexPooling].Get();
    }
    public GameObject GetExplosionVFX()
    { 
        return explosionVfx.Get();
    }
}