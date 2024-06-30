using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int objIndex;
    public Transform start, end;
    public float height = 2;
    public void Spawn(float timeFly)
    {
        if (start == null)
            start = transform;
        if (end == null)
            end = EndPointItem.Instance.transform;

        Vector3 _endPos = end.position + new Vector3((float)Random.Range(1, 10)/100, (float)Random.Range(1, 10) / 100, 0);

        GameObject obj = PoolingsMng.Instance.GetItem(objIndex);
        obj.transform.position = start.position;
        obj.gameObject.SetActive(true);
        obj.GetComponent<ParabolicMovement>().MoveParabolic(start.position, _endPos, transform.position.y + height, timeFly);

        VFX_Spawn.Instance.Play(transform.position);
    }
}
