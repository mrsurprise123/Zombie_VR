using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;
using VRTK;

public enum GrabedObjectType
{
    None,
    Other,
    Book,
    Pistol,
    Belt,
    Magazine,
    Bomb,
}
public enum HandAnimationStateType
{
    None,
    Pistol,
}
public class HandManager : MonoBehaviour {
    /// <summary>
    /// Listhen to the grab button to grab an object
    /// </summary>
    public EventDefine CatchEvent;
    public EventDefine UseBombEvent;
    public EventDefine UsePistolEvent;
    public StateModel[] stateModels;
    [System.Serializable]
    public class StateModel
    {
        public HandAnimationStateType Statetype;
        public GameObject ob;
    }
    private bool isGripButtonPressed = false;
    public GameObject GrabedObject = null;//The grabed object
    public GrabedObjectType this_grabedObjectType = GrabedObjectType.None;
    private Animator Hand_animator;
    private bool isHandeCollideST = false;//To see if the hand has collide somethings
    private bool isGrabbedPistol = false;//If this hand has grabbed a pistol
    public EventDefine shootEvent;
    public GameObject go_Bomb;
    private bool m_IsUseBomb = false;
    private VRTK_ControllerEvents controllerEvents;
    public float m_ThrowMulitiple = 1f;

    void Awake()
    {
        Hand_animator = GetComponent<Animator>();
        EventCenter.AddListener<bool>(CatchEvent, IsCanCatch);
        EventCenter.AddListener(shootEvent, shot);
        EventCenter.AddListener(UseBombEvent, UseBomb);
        EventCenter.AddListener(UsePistolEvent, UsePistol);
        controllerEvents = GetComponentInParent<VRTK_ControllerEvents>();
    }
    private void shot()
    {
        if(Hand_animator.GetInteger("State") != (int)HandAnimationStateType.Pistol)
            return;
        Hand_animator.SetTrigger("Shoot");
    }
    private void OnDestory()
    {
        EventCenter.RemoveListener<bool>(CatchEvent, IsCanCatch);
        EventCenter.RemoveListener(shootEvent, shot);
        EventCenter.RemoveListener(UseBombEvent, UseBomb);
        EventCenter.RemoveListener(UsePistolEvent, UsePistol);
    }
    //To see if the object can be grabbed and make abimator play catch animation
    private void IsCanCatch(bool iscatch)
    {
        if (iscatch == false)
        {
            if (GrabedObject != null && this_grabedObjectType == GrabedObjectType.Bomb)
            {
                //代表拿的是炸弹
                ThrowBomb();
            }
        }
        if (iscatch)//release the grabed object
        {
            if(GrabedObject != null)
            {
                if(this_grabedObjectType == GrabedObjectType.Other)
                {
                    GrabedObject.transform.parent = null;
                    GrabedObject = null;
                    this_grabedObjectType = GrabedObjectType.None;
                }
                else if(this_grabedObjectType == GrabedObjectType.Book)
                {//if the grabbed the object is book, make it return to its original position or the bookstand
                    if (GrabedObject.GetComponent<Book>().isTriggered)
                    {
                        GrabedObject.GetComponent<Book>().putbookOnBookStand();
                    }
                    else
                    {
                        GrabedObject.transform.parent = null;
                        GrabedObject.transform.position = GrabedObject.GetComponent<Book>().original_position;
                        GrabedObject.transform.rotation = GrabedObject.GetComponent<Book>().original_rotation;
                    }
                    GrabedObject = null;
                    this_grabedObjectType = GrabedObjectType.None;
                }
                else if (this_grabedObjectType == GrabedObjectType.Belt || this_grabedObjectType == GrabedObjectType.Magazine)
                {
                    GrabedObject.transform.parent = null;
                    GrabedObject.GetComponent<Rigidbody>().useGravity = true;
                    GrabedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    GrabedObject = null;
                    this_grabedObjectType = GrabedObjectType.None;
                }
                return;
            }
        }
        if(GrabedObject == null)
            Hand_animator.SetBool("Catch", iscatch);
        isGripButtonPressed = iscatch;
        PistolSwitchHand();
    }
    private void ThrowBomb()
    {
        //更新炸弹数量
        AmmoUI.Instance.UpdateBombNumber();

        GrabedObject.transform.parent = null;
        GrabedObject.AddComponent<Rigidbody>();
        Hand_animator.SetBool("Catch", false);
        this_grabedObjectType = GrabedObjectType.None;

        Vector3 velocity = controllerEvents.GetVelocity();
        Vector3 angularVelocity = controllerEvents.GetAngularVelocity();

        GrabedObject.GetComponent<Rigidbody>().velocity = transform.parent.parent.TransformDirection(velocity) * m_ThrowMulitiple;
        GrabedObject.GetComponent<Rigidbody>().angularVelocity = transform.parent.parent.TransformDirection(angularVelocity);
        GrabedObject.GetComponent<Bomb>().IsThrow = true;
        GrabedObject = null;
        m_IsUseBomb = false;

        //UsePistol();
    }
    /// <summary>
    /// 1. set the catch animation
    /// 2. if the other hand is in the catch anmation, release its catch animation
    /// </summary>
    /// <param name="value"></param>
    public void KeepCatchAnimation(bool value)
    {
        if(GrabedObject != null)
        {
            GrabedObject = null;
            this_grabedObjectType = GrabedObjectType.None;
        }
        Hand_animator.SetBool("Catch", value);
    }
    private void UseBomb()
    {
        if (AmmoUI.Instance.isHasBomb() == false) return;
        if (GrabedObject != null)
        {
            if (this_grabedObjectType == GrabedObjectType.Pistol)
            {
                UnLoadPistol();
            }
            else if (this_grabedObjectType == GrabedObjectType.Belt || this_grabedObjectType == GrabedObjectType.Magazine)
            {
                GrabedObject.transform.parent = null;
                GrabedObject.GetComponent<Rigidbody>().useGravity = true;
                GrabedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                GrabedObject = null;
                this_grabedObjectType = GrabedObjectType.None;
            }
            else if (this_grabedObjectType == GrabedObjectType.Bomb)
            {
                return;
            }
        }
        Transform target = transform.parent.Find("BombTarget");
        GameObject bomb = Instantiate(go_Bomb, transform.parent);
        bomb.transform.localPosition = target.localPosition;
        bomb.transform.localRotation = target.localRotation;
        bomb.transform.localScale = target.localScale;

        GrabedObject = bomb;
        this_grabedObjectType = GrabedObjectType.Bomb;
        Hand_animator.SetBool("Catch", true);
        m_IsUseBomb = true;
    }
    public void UnUseBomb()
    {
        m_IsUseBomb = false;
        Destroy(GrabedObject);
        GrabedObject = null;
        this_grabedObjectType = GrabedObjectType.None;
        Hand_animator.SetBool("Catch", false);
    }

    private void OnTriggerStay(Collider other)//Collide the object that can be grabed and stay in that collider
    {
        if(other.tag == "Others" || other.tag == "Book" || other.tag == "Pistol" || other.tag == "Belt" || other.tag == "Magazine")
        {
            isHandeCollideST = true;
        }
        //make the other pbject highlighted
        if(other.GetComponent<Highlighter>() != null)
        {
            other.GetComponent<Highlighter>().On(Color.green);
        }
        if(other.tag == "Others" && isGripButtonPressed && GrabedObject == null)
        {
            ProcessGrab(other);
            this_grabedObjectType = GrabedObjectType.Other;
        }
        else if(other.tag == "Book" && isGripButtonPressed && GrabedObject == null)
        {
            ProcessGrab(other);
            this_grabedObjectType = GrabedObjectType.Book;
        }
        else if(other.tag == "Pistol" && isGripButtonPressed && GrabedObject == null)
        {
            Destroy(other.gameObject);
            UsePistol();
        }
        else if (other.tag == "Belt" && isGripButtonPressed && GrabedObject == null)
        {
            ProcessGrab(other);
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            this_grabedObjectType = GrabedObjectType.Belt;
        }
        else if (other.tag == "Magazine" && isGripButtonPressed && GrabedObject == null)
        {
            ProcessGrab(other);
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            this_grabedObjectType = GrabedObjectType.Magazine;
        }
    }
    //Use the pistol
    private void UsePistol()
    {
        if (GrabedObject != null)
        {
            if (this_grabedObjectType == GrabedObjectType.Belt || this_grabedObjectType == GrabedObjectType.Magazine)
            {
                GrabedObject.transform.parent = null;
                GrabedObject.GetComponent<Rigidbody>().useGravity = true;
                GrabedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                GrabedObject = null;
                this_grabedObjectType = GrabedObjectType.None;
            }
            else if (this_grabedObjectType == GrabedObjectType.Bomb)
            {
                UnUseBomb();
            }
            else if (this_grabedObjectType == GrabedObjectType.Pistol)
            {
                return;
            }
        }
        isGrabbedPistol = true;
        Hand_animator.SetBool("Catch", false);
        this_grabedObjectType = GrabedObjectType.Pistol;
        GrabedObject = stateModels[0].ob;
        setSate(HandAnimationStateType.Pistol);
    }
    //Set the state param of the Animator
    private void setSate(HandAnimationStateType stateType)
    {
        Hand_animator.SetInteger("State", (int)stateType);
        foreach(var item in stateModels)
        {
            if(item.Statetype == stateType && item.ob.activeSelf == false)
            {
                item.ob.SetActive(true);
            }
            else if (item.ob.activeSelf)
            {
                item.ob.SetActive(false);
            }
        }
    }
    //Switch to the other hand to hold the pistol
    private void PistolSwitchHand()
    {
        if(GrabedObject == null && isHandeCollideST == false && isGripButtonPressed == false)
        {
            HandManager[] handManagers = GameObject.FindObjectsOfType<HandManager>();
            foreach(var item in handManagers)
            {
                if(item != this)
                {
                    if (item.isGrabbedPistol)
                    {
                        UsePistol();
                        item.UnLoadPistol();
                        stateModels[0].ob.GetComponent<Pistol>().CurrentBulletCount = item.stateModels[0].ob.GetComponent<Pistol>().CurrentBulletCount;
                    }
                    if (item.m_IsUseBomb)
                    {
                        item.UnUseBomb();
                        UseBomb();
                    }
                }
            }

        }
    }
    //UnLoad the pistol from hand
    public void UnLoadPistol()
    {
        isGrabbedPistol = false;
        Hand_animator.SetBool("Catch", false);
        this_grabedObjectType = GrabedObjectType.None;
        GrabedObject = null;
        setSate(HandAnimationStateType.None);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Highlighter>() != null)
        {
            other.GetComponent<Highlighter>().Off();
        }
        isHandeCollideST = false;
    }
    private void ProcessGrab(Collider other)
    {
        if (other.transform.parent != null)//if the other hand is grabing this collider
        {
            if (other.transform.parent.tag == "ControllerLeft" || other.transform.parent.tag == "ControllerRight")
            {
                other.transform.parent.GetComponentInChildren<HandManager>().KeepCatchAnimation(false);//To let the hand grab the collider realease the grab animation
            }
        }
        KeepCatchAnimation(true);
        other.gameObject.transform.parent = transform.parent;
        GrabedObject = other.transform.gameObject;
    }
}
