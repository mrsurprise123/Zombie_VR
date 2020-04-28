using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Watch : MonoBehaviour {
    private Transform target;
    private bool isFound = false;
    private Text Hp;

    private void Awake()
    {
        Hp = GetComponentInChildren<Text>();
        EventCenter.AddListener<int>(EventDefine.UpdateHpUI,UpdateHp);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.UpdateHpUI, UpdateHp);
    }
	private void FixedUpdate()
    {
        if(GameObject.FindGameObjectWithTag("WatchTarget") != null && isFound == false)
        {
            target = GameObject.FindGameObjectWithTag("WatchTarget").transform;
            isFound = true;
        }
        if(target != null)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }

    private void UpdateHp(int hp)
    {
        Hp.text = hp.ToString();
    }
}
