using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class RightController : BaseControllerManager
{
    public override void GripPressed()
    {
        EventCenter.Broadcast(EventDefine.RightHandCatch, true);
    }

    public override void GripReleased()
    {
        EventCenter.Broadcast(EventDefine.RightHandCatch, false);
    }

    public override void TouchpadPressed()
    {
        
    }

    public override void TouchpadReleased()
    {
        EventCenter.Broadcast(EventDefine.RightReload);
    }

    public override void TriggerPressed()
    {
        EventCenter.Broadcast(EventDefine.RightShoot);
    }

    public override void TriggerReleased()
    {
        
    }
}
