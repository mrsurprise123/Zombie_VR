using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieController : MonoBehaviour {

    private NavMeshAgent m_agent;
    private Animator m_animator;
    private Transform target;
    private bool isFirstAttack = true;
    public float WalkSpeed = 0.6f;
    public float RunSpeed = 2f;
    public float distance = 1f;
    private float Timer = 0.0f;
    public float AttackInterval = 2f;
    private bool IsAttacking = false;
    public float m_HitDealyTime = 2.0f;
    private bool m_IsHitting = false;
    private bool m_IsDeath = false;
    public AudioClip audio_Attack;
    public AudioClip audio_Walk;
    private AudioSource m_AudioSource;
    public bool IsDeath
    {
        get
        {
            return m_IsDeath;
        }
    }
    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        target = Camera.main.transform;
        EventCenter.AddListener<Vector3>(EventDefine.BombBrust, BombBrust);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<Vector3>(EventDefine.BombBrust, BombBrust);
    }
    private void BombBrust(Vector3 brustPos)
    {
        if (Vector3.Distance(transform.position, brustPos) < 10.0f)
        {
            BodyPartDrop[] drops = transform.GetComponentsInChildren<BodyPartDrop>();
            foreach (var item in drops)
            {
                item.Hit();
            }
            Death();
        }
    }
    private void FixedUpdate()
    {
        if (m_IsDeath)
            return;
        if (m_IsHitting)
            return;
        Vector3 temp = new Vector3(target.position.x, transform.position.y, target.position.z);
        if(Vector3.Distance(transform.position, temp) < distance)
        {
            if(m_agent.isStopped == false)
            {
                m_agent.isStopped = true;
            }
            if(IsAttacking == false)
            {
                Timer += Time.deltaTime;
                if(Timer >= AttackInterval)
                {
                    Timer = 0.0f;
                    IsAttacking = true;
                    Attack();
                }
            }
            if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_BlendTree") == false)
            {
                if (isFirstAttack)
                {
                    distance += 0.6f;
                    isFirstAttack = false;
                    IsAttacking = true;
                    Attack();
                }
                else
                {
                    IsAttacking = false;
                }
            }
        }
        else
        {
            if (m_agent.isStopped)
            {
                distance -= 0.6f;
                isFirstAttack = true;
                m_agent.isStopped = false;
                RandomWalkOrRun();
            }
            m_agent.SetDestination(Camera.main.transform.position);
        }
       
    }
    private void RandomWalkOrRun()
    {
        int ran = Random.Range(0, 2);
        if(ran == 0)
        {
            WalkAnim();
            m_agent.speed = WalkSpeed;
        }
        else
        {
            RunAnim();
            m_agent.speed = RunSpeed;
        }
    }
    private void WalkAnim()
    {
        if (m_AudioSource.isPlaying == false)
        {
            m_AudioSource.clip = audio_Walk;
            m_AudioSource.Play();
        }
        PlayAnim(3, "Walk", "WalkValue");
    }
    private void RunAnim()
    {
        PlayAnim(2, "Run", "RunValue");;
    }
    private void PlayAnim(int clipCount, string TriggerName, string FloatName)
    {
        float value = 1.0f / (clipCount - 1);
        m_animator.SetTrigger(TriggerName);
        m_animator.SetFloat(FloatName, value * Random.Range(0, clipCount));
    }
    private void Start()
    {
        RandomWalkOrRun();
    }

    private void Attack()
    {
        if (m_AudioSource.isPlaying == false)
        {
            m_AudioSource.clip = audio_Attack;
            m_AudioSource.Play();
        }
        EventCenter.Broadcast(EventDefine.UpdateHP, -10);
        EventCenter.Broadcast(EventDefine.ScreenBlood);
        Vector3 temp1 = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(temp1);
        PlayAnim(6, "Attack", "AttackValue");
    }
    IEnumerator HitDealy()
    {
        yield return new WaitForSeconds(m_HitDealyTime);
        if (m_IsDeath == false)
        {
            m_agent.isStopped = false;
            m_IsHitting = false;
            RandomWalkOrRun();
        }
    }
    public void HitLeft()
    {
        m_animator.SetTrigger("HitLeft");
        m_agent.isStopped = true;
        m_animator.SetTrigger("Idle");
        m_IsHitting = true;
        StartCoroutine(HitDealy());
    }
    public void HitRight()
    {
        m_animator.SetTrigger("HitRight");
        m_agent.isStopped = true;
        m_animator.SetTrigger("Idle");
        m_IsHitting = true;
        StartCoroutine(HitDealy());
    }
    public void Death()
    {
        if (m_IsDeath) return;
        PlayAnim(4, "Death", "DeathValue");
        m_agent.isStopped = true;
        m_IsDeath = true;
        Destroy(m_agent);
        EventCenter.Broadcast(EventDefine.ZombieDeath);
    }
    public void Hit()
    {
        PlayAnim(3, "Hit", "HitValue");
        m_agent.isStopped = true;
        m_animator.SetTrigger("Idle");
        m_IsHitting = true;
        StartCoroutine(HitDealy());
    }
}
