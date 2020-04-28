using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainSceneUIManager : MonoBehaviour {
    private GameObject startPanel;
    private GameObject aboutPanel;
    private GameObject gesturePanel;
    private Button newGameButton;
    private bool startPanelShowed = false;
	// Use this for initialization
	void Awake () {
        startPanel = transform.Find("StartPanel").gameObject;
        aboutPanel = transform.Find("AboutPanel").gameObject;
        gesturePanel = transform.Find("GesturePanel").gameObject;
        newGameButton = startPanel.transform.Find("newGame").GetComponent<Button>();
        newGameButton.onClick.AddListener(() => 
        {
            newGameButton.gameObject.SetActive(false);
            EventCenter.Broadcast(EventDefine.StartLoadScene);
        });
        EventCenter.AddListener<bool>(EventDefine.IsShowAboutPanel,IsShowAboutPanel);
        EventCenter.AddListener<bool>(EventDefine.IsShowStartPanel, IsShowStartPanel);
        EventCenter.AddListener<bool>(EventDefine.IsGesturePanel, IsGesturePanel);
        EventCenter.AddListener(EventDefine.StartGame,StartGame);
    }
    // use voice to start a game
    private void StartGame()
    {
        if (startPanelShowed)
        {
            newGameButton.gameObject.SetActive(false);
            EventCenter.Broadcast(EventDefine.StartLoadScene);
        }
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<bool>(EventDefine.IsShowAboutPanel, IsShowAboutPanel);
        EventCenter.RemoveListener<bool>(EventDefine.IsShowStartPanel, IsShowStartPanel);
        EventCenter.RemoveListener<bool>(EventDefine.IsGesturePanel, IsGesturePanel);
        EventCenter.RemoveListener(EventDefine.StartGame, StartGame);
    }
    //if show the panel
    private void IsShowPanel(bool value, GameObject ob)
    {
        if (value)
        {
            ob.transform.DOLocalMoveY(0, 0.5F);
        }
        else
        {
            ob.transform.DOLocalMoveY(995, 0.5F);
        }
    }
    //show the Panel
    private void IsShowAboutPanel(bool value)
    {
        IsShowPanel(value, aboutPanel);
    }
    private void IsShowStartPanel(bool value)
    {
        startPanelShowed = value;
        IsShowPanel(value, startPanel);
    }
    private void IsGesturePanel(bool value)
    {
        IsShowPanel(value, gesturePanel);
    }



}
