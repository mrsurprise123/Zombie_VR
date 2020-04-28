using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject[] go_ZombiesPre;

    public Roads[] roads;
    [System.Serializable]
    public class Roads
    {
        public GameObject[] gos;
    }
    /// <summary>
    /// 道路设置为可以瞬移的次数
    /// </summary>
    private int m_RoadCanMoveCount = 0;
    private bool m_IsWearBelt = false;
    private bool m_IsWearPistol = false;
    /// <summary>
    /// 生成僵尸的次数
    /// </summary>
    private int m_SpawnZombieCount = 0;
    /// <summary>
    /// 僵尸死亡数量
    /// </summary>
    private int m_ZombieDeathCount = 0;

    private void Awake()
    {
        foreach (var road in roads)
        {
            foreach (var go in road.gos)
            {
                go.layer = 2;
                go.AddComponent<Road>();
            }
        }
        foreach (var item in GetComponentsInChildren<BoxCollider>())
        {
            Destroy(item.GetComponent<BoxCollider>());
            Destroy(item.GetComponent<MeshFilter>());
            Destroy(item.GetComponent<MeshRenderer>());
        }
        EventCenter.AddListener(EventDefine.WearBelt, WearBelt);
        EventCenter.AddListener(EventDefine.WearPistol, WearPistol);
        EventCenter.AddListener(EventDefine.SpawnZombies, SpawnZombies);
        EventCenter.AddListener(EventDefine.ZombieDeath, ZombieDeath);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.WearBelt, WearBelt);
        EventCenter.RemoveListener(EventDefine.WearPistol, WearPistol);
        EventCenter.RemoveListener(EventDefine.SpawnZombies, SpawnZombies);
        EventCenter.RemoveListener(EventDefine.ZombieDeath, ZombieDeath);
    }
    /// <summary>
    /// 僵尸死亡
    /// </summary>
    private void ZombieDeath()
    {
        m_ZombieDeathCount++;
        if (m_ZombieDeathCount == transform.GetChild(m_SpawnZombieCount - 1).childCount)
        {
            //击杀僵尸的数量等于上一次生成僵尸的数量
            SetRoadCanMove();
        }
    }
    /// <summary>
    /// 生成僵尸
    /// </summary>
    private void SpawnZombies()
    {
        if (m_SpawnZombieCount > transform.childCount - 1) return;

        for (int i = 0; i < transform.GetChild(m_SpawnZombieCount).childCount; i++)
        {
            Vector3 spawnPos = transform.GetChild(m_SpawnZombieCount).GetChild(i).position;
            int ran = Random.Range(0, go_ZombiesPre.Length);
            Instantiate(go_ZombiesPre[ran], spawnPos, Quaternion.identity);
        }
        m_SpawnZombieCount++;
    }
    private void WearBelt()
    {
        m_IsWearBelt = true;
        if (m_IsWearBelt && m_IsWearPistol)
        {
            SetRoadCanMove();
        }
    }
    private void WearPistol()
    {
        m_IsWearPistol = true;
        if (m_IsWearBelt && m_IsWearPistol)
        {
            SetRoadCanMove();
        }
    }
    /// <summary>
    /// 设置道路可以移动
    /// </summary>
    private void SetRoadCanMove()
    {
        if (m_RoadCanMoveCount > roads.Length - 1) return;
        foreach (var go in roads[m_RoadCanMoveCount].gos)
        {
            go.layer = 0;
        }
        m_RoadCanMoveCount++;
    }
}
