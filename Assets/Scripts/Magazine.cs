using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour {

    private int BulletNumber = 6;
    public void SetBulletNumber(int number)
    {
        BulletNumber = number;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bucket")
        {
            if(transform.parent != null && transform.parent.GetComponentInChildren<HandManager>() != null)
            {
                Destroy(gameObject);
                transform.parent.GetComponentInChildren<HandManager>().KeepCatchAnimation(false);
                AmmoUI.Instance.UpdateBulletNumber(BulletNumber);
            }
        }
    }
}
