using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float delay = 3;
    private void Start()
    {
        GetComponent<Item>().onBeGrab += StartTimer;
        GetComponent<ParabolicMovement>().onEndMove += Explosion;
    }
    private void StartTimer()
    {
        Invoke(nameof(Explosion), delay);
    }
    void Explosion()
    {
        GameObject vfx = PoolingsMng.Instance.GetExplosionVFX();
        vfx.transform.position = transform.position;
        vfx.SetActive(true);
        vfx.GetComponent<Explosion>().Fire();
        transform.localScale = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player.Instance.gameObject)
        {
            Explosion();
        }
    }
}