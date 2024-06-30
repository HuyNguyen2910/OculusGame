using System.Collections;
using TigerForge;
using UnityEngine;

public class HandGrab : MonoBehaviour
{
    public OVRInput.Controller controller;
    public Transform end;

    public Item itemCanGrabing = null;
    public Item itemGrabing = null;
    public Transform display;
    RaycastHit hit;

    public LayerMask layerMask;
    // Start is called before the first frame update

    Vector3 pre;
    public Vector3 dir;
    // Update is called once per frame
    void Update()
    {
        pre = transform.position;

        if (OVRInputCtrl.ButtonHandTrigger(controller))
        {
            if (itemGrabing == null)
            {
                if (itemCanGrabing != null)
                { 
                    itemGrabing = itemCanGrabing;
                    itemGrabing.BeGrab(end);
                    EventManager.EmitEvent(EventKey.OnCrabItem.ToString());
                    AudioMng.Instance.Play(0);
                }
            }
        }
        if (OVRInputCtrl.ButtonHandTriggerUp(controller))
        {
            if (itemGrabing != null)
            {
                itemGrabing.Free(dir);
                itemGrabing = null;
                AudioMng.Instance.Play(1);
            }
        }

        Debug.DrawRay(transform.position, (end.position - transform.position).normalized, Color.green);
        if (Physics.Raycast(transform.position, (end.position - transform.position).normalized, out hit, layerMask, 100))
        {
            if (hit.collider.GetComponent<Item>() != null)
            {
                display.position = hit.point;
                itemCanGrabing = hit.collider.GetComponent<Item>();
                itemCanGrabing.ToggleOutline(true);
            }
            else
            {
                DisposeItenCanGrab();
            }
        }
        else
        {
            DisposeItenCanGrab();
        }
    }
    private void FixedUpdate()
    {
        dir = transform.position - pre;
    }
    void DisposeItenCanGrab()
    {
        if (itemCanGrabing != null)
        {
            itemCanGrabing.ToggleOutline(false);
            itemCanGrabing = null;
        }
        display.position = (end.position - transform.position).normalized * 1;
    }
}
