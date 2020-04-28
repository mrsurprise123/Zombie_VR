using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Edwon.VR;
using Edwon.VR.Gesture;

public class GestureInfPanel : MonoBehaviour {
    private VRGestureSettings gestureSettings;
    private VRGestureRig gestureRig;
    private Button Back_Button;
    public GameObject gestureItem;
    private void Awake()
    {
        gestureSettings = Utils.GetGestureSettings();
        gestureRig = VRGestureRig.GetPlayerRig(gestureSettings.playerID);
        EventCenter.AddListener(EventDefine.ShowGestureInfPanel, ShowGestureInfPanel);
        Init();
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowGestureInfPanel, ShowGestureInfPanel);
    }
    //Init the panel
    private void Init()
    {
        Back_Button = GameObject.Find("Back_Button").GetComponent<Button>();
        Back_Button.onClick.AddListener(() => {
            EventCenter.Broadcast(EventDefine.ShowGestureMainPanel);
            gameObject.SetActive(false);
        });
        foreach (var gesture in GetGestures())
        {
            GameObject ob = Instantiate(gestureItem,transform.Find("ParentGesture"));
            ob.GetComponent<GestureInfItem>().Init(gesture);
        }
        gameObject.SetActive(false);
    }
	//get gestures
    private List<Gesture> GetGestures()
    {
        gestureSettings.RefreshGestureBank(true);
        return gestureSettings.gestureBank;
    }
    private void ShowGestureInfPanel()
    {
        gestureRig.uiState = VRGestureUIState.Gestures;
        gameObject.SetActive(true);
    }
}
