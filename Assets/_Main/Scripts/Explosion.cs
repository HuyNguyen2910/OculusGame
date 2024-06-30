using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particleSystems;
    public void Fire()
    {
        Invoke(nameof(Deactivate), 5);
    }
    void Deactivate()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player.Instance.gameObject)
        {
            GameCtrl.Instance.Live--;
        }
    }
}
