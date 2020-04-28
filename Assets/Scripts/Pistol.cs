using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pistol : MonoBehaviour {
    public EventDefine shootEvent;
    public EventDefine reloadEvent;
    public Transform start_Pos;//The raycas line start postion
    private LineRenderer lineRender;
    public GameObject sphere;
    public GameObject effect_hitOtherMask;
    public GameObject effect_hitOther;
    public GameObject effect_fire;
    public GameObject magazine;
    public GameObject effect_Blood;
    private Animator animator;
    private RaycastHit hit;
    public int CurrentBulletCount = 6;
    public AudioClip audio_Shot;
    private AudioSource m_AudioSource;
    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        lineRender = GetComponent<LineRenderer>();
        EventCenter.AddListener(shootEvent, shoot);
        EventCenter.AddListener(reloadEvent, Reload);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(shootEvent, shoot);
        EventCenter.RemoveListener(reloadEvent, Reload);
    }
    private void FixedUpdate()
    {
        if(Physics.Raycast(start_Pos.position,start_Pos.forward,out hit, 100000, 1 << 0 | 1 << 2 | 1 << 5))
        {
            DrawLine(start_Pos.position,hit.point,Color.red);
            sphere.SetActive(true);
            sphere.transform.position = hit.point;
        }
        else
        {
            DrawLine(start_Pos.position, start_Pos.forward * 100000, Color.green);
            sphere.SetActive(false);
        }
    }
    //Draw the line from the position of pistol
    private void DrawLine(Vector3 startPosition,Vector3 endPosition,Color color)
    {
        lineRender.positionCount = 2;
        lineRender.SetPosition(0, startPosition);
        lineRender.SetPosition(1, endPosition);
        lineRender.startWidth = 0.001f;
        lineRender.endWidth = 0.001f;
        lineRender.material.color = color;
    }
    private void Reload()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            return;
        if (gameObject.activeSelf == false)
            return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PistolFire"))
            return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PistolReload"))
            return;
        if (GameObject.FindObjectOfType<RadialMenuManager>() != null)
            if (GameObject.FindObjectOfType<RadialMenuManager>().transform.localScale != Vector3.zero)
                return;
        int temp = CurrentBulletCount;
        CurrentBulletCount = AmmoUI.Instance.ReloadMagazine();
        if(CurrentBulletCount != 0)
        {
            animator.SetTrigger("Reload");
            GameObject ob = Instantiate(magazine, transform.Find("Pistol3Magazine").position, transform.Find("Pistol3Magazine").rotation);
            ob.GetComponent<Magazine>().SetBulletNumber(temp);
        }
        
    }
    //To make the pistol can shooot
    private void shoot()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            return;
        if (gameObject.activeSelf == false)
            return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PistolFire"))
            return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PistolReload"))
            return;
        if (CurrentBulletCount <= 0)
            return;
        CurrentBulletCount--;
        animator.SetTrigger("Shoot");
        if (m_AudioSource.isPlaying == false)
        {
            m_AudioSource.clip = audio_Shot;
            m_AudioSource.Play();
        }
        Destroy(Instantiate(effect_fire, start_Pos.position, start_Pos.rotation), 1.5f);
        if(hit.collider != null)
        {
            if(hit.collider.tag == "Zombie")
            {
                if (hit.transform.GetComponent<BodyPartDrop>() != null)
                {
                    hit.transform.GetComponent<BodyPartDrop>().Hit();
                }
                if (hit.transform.GetComponent<ZombieHit>() != null)
                {
                    hit.transform.GetComponent<ZombieHit>().Hit();
                }
                Destroy(Instantiate(effect_Blood, hit.point, Quaternion.LookRotation(hit.normal)), 2f);
            }
            else
            {
                GameObject ob = Instantiate(effect_hitOtherMask, hit.point, Quaternion.LookRotation(hit.normal));
                ob.transform.parent = hit.transform;
                Destroy(Instantiate(effect_hitOther, hit.point, Quaternion.LookRotation(hit.normal)),2);
            }
        }
    }
}
