using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillChoosePanel : MonoBehaviour {
    private string m_gestureName;
    private void Update()
    {
        for (int i = 0; i < transform.Find("Parent").childCount; i++)
        {
            if (transform.Find("Parent").GetChild(i).GetComponent<Toggle>().isOn)
            {
                transform.Find("Parent").GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                transform.Find("Parent").GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    private void Init()
    {
        transform.Find("Back_Button").GetComponent<Button>().onClick.AddListener(()=>{
            for (int i = 0; i < transform.Find("Parent").childCount; i++)
            {
                if (transform.Find("Parent").GetChild(i).GetComponent<Toggle>().isOn)
                {
                    GestureSkillManager.ChangeSkillName(m_gestureName, transform.Find("Parent").GetChild(i).name);
                }
            }
            EventCenter.Broadcast(EventDefine.ShowGestureInfPanel);
            gameObject.SetActive(false);
        });
        gameObject.SetActive(false);
    }
	private void Awake()
    {
        EventCenter.AddListener<string>(EventDefine.ShowSkillChoosePanel, ShowSkillChoosePanel);
        Init();
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<string>(EventDefine.ShowSkillChoosePanel, ShowSkillChoosePanel);
    }
    private void ShowSkillChoosePanel(string gestureName)
    {
        m_gestureName = gestureName;
        gameObject.SetActive(true);
        string skillName = GestureSkillManager.GetSkillNameByGestureName(gestureName);
        for (int i = 0; i < transform.Find("Parent").childCount; i++)
        {
            if(transform.Find("Parent").GetChild(i).name == skillName)
            {
                transform.Find("Parent").GetChild(i).GetComponent<Toggle>().isOn = true;
                transform.Find("Parent").GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                transform.Find("Parent").GetChild(i).GetComponent<Toggle>().isOn = false;
                transform.Find("Parent").GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
