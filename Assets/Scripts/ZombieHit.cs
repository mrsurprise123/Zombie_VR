using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyPartType
{
    Left,
    Right,
    Head,
    Other,
}
public class ZombieHit : MonoBehaviour
{
    /// <summary>
    /// 部位类型
    /// </summary>
    public BodyPartType partType;
    public ZombieController Controller;

    public void Set(BodyPartType type, ZombieController controller)
    {
        partType = type;
        Controller = controller;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Skill")
        {
            Controller.Death();
            Destroy(GetComponent<ZombieHit>());
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Skill")
        {
            Controller.Death();
            Destroy(GetComponent<ZombieHit>());
        }
    }
    /// <summary>
    /// 射中的处理
    /// </summary>
    public void Hit()
    {
        if (Controller.IsDeath) return;

        if (partType == BodyPartType.Left)
        {
            //播放左边受伤动画
            Controller.HitLeft();
            Destroy(GetComponent<ZombieHit>());
        }
        else if (partType == BodyPartType.Right)
        {
            //播放右边受伤动画
            Controller.HitRight();
            Destroy(GetComponent<ZombieHit>());
        }
        else if (partType == BodyPartType.Head)
        {
            //死亡
            Controller.Death();
            Destroy(GetComponent<ZombieHit>());
        }
        else if (partType == BodyPartType.Other)
        {
            //播放受伤动画
            Controller.Hit();
        }
    }
}
