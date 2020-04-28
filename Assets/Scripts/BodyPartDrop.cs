using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartDrop : MonoBehaviour
{
    public GameObject go_Firend;

    public void SetFriend(GameObject go)
    {
        go_Firend = go;
    }
    /// <summary>
    /// When been hit
    /// </summary>
    public void Hit()
    {
        BodyPartDrop[] arr = transform.parent.GetComponentsInChildren<BodyPartDrop>();

        foreach (var item in arr)
        {
            item.go_Firend.SetActive(false);
            item.transform.parent = null;
            item.transform.GetChild(0).gameObject.SetActive(true);
            item.gameObject.AddComponent<Rigidbody>();
            Destroy(item);
        }
    }
}
