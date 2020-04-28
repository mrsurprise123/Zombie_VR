using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BookType
{
    StartSceneBook,
    AboutSceneBook,
    GestureSceneBook,
}
/// <summary>
/// The script the deal with operations of the three books
/// </summary>
public class Book : MonoBehaviour {
    public BookType this_BookType;
    public Vector3 original_position;
    public Quaternion original_rotation;
    public bool isTriggered = false;//if the book collide the bookstand
    private GameObject bookStand;//The object of the bookstand
    void Awake()
    {
        original_position = transform.position;
        original_rotation = transform.rotation;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "BookStand")
        {
            isTriggered = true;
            bookStand = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "BookStand")
        {
            isTriggered = false;
            bookStand = null;
        }
    }
    //Activate the UI according the booktype
    private void ActivateUI(bool value)
    {
        switch (this_BookType)
        {
            case BookType.AboutSceneBook:
                EventCenter.Broadcast(EventDefine.IsShowAboutPanel,value);
                break;
            case BookType.StartSceneBook:
                EventCenter.Broadcast(EventDefine.IsShowStartPanel, value);
                break;
            case BookType.GestureSceneBook:
                EventCenter.Broadcast(EventDefine.IsGesturePanel, value);
                break;
        }
    }
    public void putbookOnBookStand()//To put book on the bookstand
    {
        if(bookStand.GetComponentInChildren<Book>() != null)
        {
            //if there is already a book on the bookstand, release it
            bookStand.GetComponentInChildren<Book>().ReleaseFromBookStand();
        }
        transform.parent = bookStand.transform;
        transform.position = bookStand.transform.GetChild(0).position;
        transform.rotation = bookStand.transform.GetChild(0).rotation;
        ActivateUI(true);
    }
    public void ReleaseFromBookStand()//To release the book from the bookstand
    {
        transform.parent = null;
        transform.position = original_position;
        transform.rotation = original_rotation;
        ActivateUI(false);
    }

    void Update()
    {
        if(transform.parent != null && bookStand != null)
        {
            if(transform.parent != bookStand.transform)
            {
                ActivateUI(false);
            }
        }
    }
	
}
