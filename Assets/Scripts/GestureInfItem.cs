using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Edwon.VR.Gesture;

public class GestureInfItem : MonoBehaviour {
    private Button EditButton;
    private Gesture gesture;
    private Button SkillChoose_Button;
    private void Awake()
    {
        EditButton = transform.Find("Edit_Button").GetComponent<Button>();
        EditButton.onClick.AddListener(() => {
            EventCenter.Broadcast(EventDefine.ShowGestureEditPanel,gesture.name);
            transform.parent.parent.gameObject.SetActive(false);
        });
        SkillChoose_Button = transform.Find("SkillChoose_Button").GetComponent<Button>();
        SkillChoose_Button.onClick.AddListener(() => {
            EventCenter.Broadcast(EventDefine.ShowSkillChoosePanel, gesture.name);
            transform.parent.parent.gameObject.SetActive(false);
        });
    }
    public void Init(Gesture gesture)
    {
        this.gesture = gesture;
        OnEnable();
    }
    private void OnEnable()
    {
        if (gesture == null)
            return;
        transform.Find("Name").GetComponent<Text>().text = gesture.name;
        transform.Find("Example_number").GetComponent<Text>().text = gesture.exampleCount.ToString();
        SkillChoose_Button.GetComponent<Image>().sprite = GestureSkillManager.GetSkillSpriteByGestureName(gesture.name);
    }
}
