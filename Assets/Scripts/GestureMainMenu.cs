using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.VR;
using Edwon.VR.Gesture;
using UnityEngine.UI;

public class GestureMainMenu : MonoBehaviour {
    private Button gesture_inf;
    private Button gesture_detect;
    private VRGestureSettings gestureSettings;
    private VRGestureRig gestureRig;
    void Awake()
    {
        gestureSettings = Utils.GetGestureSettings();
        gestureRig = VRGestureRig.GetPlayerRig(gestureSettings.playerID);
        gesture_inf = transform.Find("Ges_Inf").GetComponent<Button>();
        gesture_inf.onClick.AddListener(()=> {
            EventCenter.Broadcast(EventDefine.ShowGestureInfPanel);
            gameObject.SetActive(false);
        });
        gesture_detect = transform.Find("Ges_Detect").GetComponent<Button>();
        gesture_detect.onClick.AddListener(() => {
            EventCenter.Broadcast(EventDefine.ShowGestureDetectPanel);
            gameObject.SetActive(false);
        });
        EventCenter.AddListener(EventDefine.ShowGestureMainPanel,ShowGesturePanel);
        ShowGesturePanel();
    }
    void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowGestureMainPanel, ShowGesturePanel);
    }
    private void ShowGesturePanel()
    {
        gestureRig.uiState = VRGestureUIState.Idle;
        gameObject.SetActive(true);
    }
}
