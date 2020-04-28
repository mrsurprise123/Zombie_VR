using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.VR;
using Edwon.VR.Gesture;

public abstract class BaseGestureRecognition : MonoBehaviour
{
    private VRGestureSettings gestureSettings;
    protected VRGestureRig gestureRig;

    public abstract void OnGestureDetectedEvent(string gestureName, double confidence);

    public virtual void Awake()
    {
        gestureSettings = Utils.GetGestureSettings();
        gestureRig = VRGestureRig.GetPlayerRig(gestureSettings.playerID);
        GestureRecognizer.GestureDetectedEvent += GestureRecognizer_GestureDetectedEvent;
    }
    public virtual void OnDestroy()
    {
        GestureRecognizer.GestureDetectedEvent -= GestureRecognizer_GestureDetectedEvent;
    }
    /// <summary>
    /// 当手势被检测到
    /// </summary>
    /// <param name="gestureName"></param>
    /// <param name="confidence"></param>
    /// <param name="hand"></param>
    /// <param name="isDouble"></param>
    private void GestureRecognizer_GestureDetectedEvent(string gestureName, double confidence, Handedness hand, bool isDouble = false)
    {
        OnGestureDetectedEvent(gestureName, confidence);
    }
    /// <summary>
    /// 开始手势识别
    /// </summary>
    protected void BeginRecognition()
    {
        gestureRig.BeginDetect();
    }
}
