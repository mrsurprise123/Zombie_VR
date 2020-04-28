using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "CameraRig")
        {
            other.tag = "Bucket";
            if(transform.parent != null && transform.parent.GetComponentInChildren<HandManager>() != null)
            {
                transform.parent.GetComponentInChildren<HandManager>().KeepCatchAnimation(false);
                Destroy(gameObject);
                EventCenter.Broadcast(EventDefine.WearBelt);
            }
            
        }
    }
}
