using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OVRInputCtrl
{
    /// <summary>
    /// Lấy Axis ngón cái 
    /// </summary>
    public static Vector3 AxisThumbstick(OVRInput.Controller controller)
    {
        return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);   
    }

    /// <summary>
    ///  Lấy Axis ngón trỏ
    /// </summary>
    public static float AxisIndexTrigger(OVRInput.Controller controller)
    {
        return OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);
    }

    /// <summary>
    ///  Lấy Axis ngón giữa
    /// </summary>
    public static float AxisHandTrigger(OVRInput.Controller controller)
    {
        return OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);
    }
    /// <summary>
    ///  Lấy button click ngón giữa
    /// </summary>
    public static bool ButtonHandTrigger(OVRInput.Controller controller)
    {
        return OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, controller);
    }
    /// <summary>
    ///  Lấy button up ngón giữa
    /// </summary>
    public static bool ButtonHandTriggerUp(OVRInput.Controller controller)
    {
        return OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller);
    }

    /// <summary>
    ///  Lấy button  B
    /// </summary>
    public static bool ButtonTwoClick(OVRInput.Controller controller = OVRInput.Controller.RTouch)
    {
        return OVRInput.GetUp(OVRInput.Button.Two, controller);
    }
}
