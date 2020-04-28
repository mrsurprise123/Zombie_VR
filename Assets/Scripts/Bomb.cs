using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool IsThrow = false;
    public float BrustTime = 3f;
    public GameObject effect_Brust;

    private float m_Timer = 0.0f;

    private void FixedUpdate()
    {
        if (IsThrow)
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= BrustTime)
            {
                Instantiate(effect_Brust, transform.position, transform.rotation);
                Destroy(gameObject);
                EventCenter.Broadcast(EventDefine.BombBrust, transform.position);
            }
        }
    }
}
