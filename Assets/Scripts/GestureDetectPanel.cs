using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Edwon.VR.Gesture;
using Edwon.VR;

public class GestureDetectPanel : MonoBehaviour {
    private Button back_Button;
    private Text GestureName;
    private Text GestureAccuracy;
    private VRGestureSettings gestureSettings;
    private VRGestureRig gestureRig;
    private void Awake()
    {
        gestureSettings = Utils.GetGestureSettings();
        gestureRig = VRGestureRig.GetPlayerRig(gestureSettings.playerID);
        back_Button = transform.Find("Back_Button").GetComponent<Button>();
        back_Button.onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.ShowGestureMainPanel);
            gameObject.SetActive(false);
        });
        GestureName = transform.Find("Name").GetComponent<Text>();
        GestureAccuracy = transform.Find("Accruacy").GetComponent<Text>();
        EventCenter.AddListener(EventDefine.ShowGestureDetectPanel, ShowGestureDetectPanel);
        GestureRecognizer.GestureDetectedEvent += GestureRecognizer_GestureDetectedEvent;
        gameObject.SetActive(false);
    }

    private void GestureRecognizer_GestureDetectedEvent(string gestureName, double confidence, Handedness hand, bool isDouble = false)
    {
        StartCoroutine(Delay(gestureName, confidence));
    }
    IEnumerator Delay(string gestureName, double confidence)
    {
        GestureName.text = gestureName;
        GestureAccuracy.text = confidence.ToString("F3") + "%";
        yield return new WaitForSeconds(0.5f);
        GestureName.text = "";
        GestureAccuracy.text = "%";
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowGestureDetectPanel, ShowGestureDetectPanel);
        GestureRecognizer.GestureDetectedEvent -= GestureRecognizer_GestureDetectedEvent;
    }
    private void ShowGestureDetectPanel()
    {
        gameObject.SetActive(true);
        BeginDetectMode();
    }
    //Begin to detect the gesture
    private void BeginDetectMode()
    {
        gestureRig.BeginDetect();
    }
}
