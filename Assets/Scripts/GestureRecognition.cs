using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.VR;
using Edwon.VR.Gesture;

public class GestureRecognition : BaseGestureRecognition
{
    public override void Awake()
    {
        base.Awake();
        EventCenter.AddListener<bool>(EventDefine.IsStartGestureRecognition, IsStartGestureRecognition);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        EventCenter.RemoveListener<bool>(EventDefine.IsStartGestureRecognition, IsStartGestureRecognition);
    }
    /// <summary>
    /// 是否开始手势识别
    /// </summary>
    /// <param name="value"></param>
    private void IsStartGestureRecognition(bool value)
    {
        if (value)
        {
            BeginRecognition();
        }
        else
        {
            gestureRig.uiState = VRGestureUIState.Idle;
        }
    }

    public override void OnGestureDetectedEvent(string gestureName, double confidence)
    {
        string skillName = GestureSkillManager.GetSkillNameByGestureName(gestureName);
        GameObject skill = ResourcesManager.LoadObj(skillName);
        Instantiate(skill, new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z), skill.transform.rotation);
    }
}
