using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class CaiGio : MonoBehaviour
{
    public int score => colliders.Count;
    public bool tinhdiem = false;
    public List<Collider> colliders;

    private void Update()
    {
        if (tinhdiem)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Item>())
        {
            if (!colliders.Exists(x => x == other))
            {
                colliders.Add(other);
                EventManager.EmitEvent(EventKey.OnScoreChanged.ToString());
                AudioMng.Instance.Play(2);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (colliders.Exists(x => x == other))
        {
            colliders.Remove(other);
            EventManager.EmitEvent(EventKey.OnScoreChanged.ToString());
        }
    }
}
