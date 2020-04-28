using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class LetfControllerManager : BaseControllerManager {

    public override void GripPressed()
    {
        EventCenter.Broadcast(EventDefine.LeftHandCatch, true);
    }

    public override void GripReleased()
    {
        EventCenter.Broadcast(EventDefine.LeftHandCatch, false);
    }

    public override void TouchpadPressed()
    {
        
    }

    public override void TouchpadReleased()
    {
        EventCenter.Broadcast(EventDefine.LeftRelaod);
    }

    public override void TriggerPressed()
    {
        EventCenter.Broadcast(EventDefine.LeftShoot);
    }

    public override void TriggerReleased()
    {
        
    }
}
