using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour {
    public static AmmoUI Instance;
    public int BulletNumber;
    public int BombNumber;
    private Transform target;
    private Text bullet;
    private Text bomb;
    private void Start()
    {
        bullet = transform.Find("Bullet/Text").GetComponent<Text>();
        bomb = transform.Find("Bomb/Text").GetComponent<Text>();
        target = GameObject.FindGameObjectWithTag("CameraRig").transform;
        EventCenter.AddListener(EventDefine.WearBelt, Show);
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.WearBelt, Show);
    }
    private void Awake()
    {
        Instance = this;
    }
	private void FixedUpdate()
    {
        float height = target.GetComponent<CapsuleCollider>().height;
        transform.position = new Vector3(Camera.main.transform.position.x,height, Camera.main.transform.position.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y,0);
    }
    private void Show()
    {
        gameObject.SetActive(true);
        UpdateBulletNumber(0);
        UpdateBombNumber(0);
    }
    public void UpdateBulletNumber(int number)
    {
        BulletNumber += number;
        bullet.text = BulletNumber.ToString();
    }
    public void UpdateBombNumber(int number = -1)
    {
        BombNumber += number;
        bomb.text = BombNumber.ToString();
    }
    public int ReloadMagazine()
    {
        if(BulletNumber >= 6)
        {
            UpdateBulletNumber(-6);
            return 6;
        }
        else
        {
            int temp = BulletNumber;
            BulletNumber = 0;
            UpdateBulletNumber(0);
            return temp;
        }
    }
    public bool isHasBomb()
    {
        if(BombNumber <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
