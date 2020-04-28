using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HPManager : MonoBehaviour
{
    public int HP = 100;

    private void Awake()
    {
        EventCenter.AddListener<int>(EventDefine.UpdateHP, UpdateHP);
        EventCenter.AddListener<Vector3>(EventDefine.BombBrust, BombBrust);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.UpdateHP, UpdateHP);
        EventCenter.RemoveListener<Vector3>(EventDefine.BombBrust, BombBrust);
    }
    /// <summary>
    /// 炸弹爆炸
    /// </summary>
    /// <param name="brustPos"></param>
    private void BombBrust(Vector3 brustPos)
    {
        if (Vector3.Distance(transform.position, brustPos) < 10.0f)
        {
            UpdateHP(-20);
        }
    }
    /// <summary>
    /// 更新血量
    /// </summary>
    /// <param name="count"></param>
    private void UpdateHP(int count)
    {
        if (count < 0)
        {
            if (HP <= Mathf.Abs(count))
            {
                //死亡
                HP = 0;
                Death();
            }
            else
            {
                HP += count;
            }
        }
        else
        {
            HP += count;
        }
        EventCenter.Broadcast(EventDefine.UpdateHpUI, HP);
    }
    private void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
