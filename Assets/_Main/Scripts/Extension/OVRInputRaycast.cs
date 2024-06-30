using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRInputRaycast : Raycast
{
    public static OVRInputRaycast Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void ToggeActive(bool isShow)
    {
        IsActive = isShow;
        line.enabled = isShow;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (raycastingObj != null)
        {
            if (raycastingObj.GetComponent<ButtonOVR>())
            {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
                {
                    raycastingObj.GetComponent<ButtonOVR>().OnRaycastClick();
                }
            }
        }
    }
}
