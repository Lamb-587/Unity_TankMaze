using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObj : MonoBehaviour
{
    //用于实例化的子弹对象；
    public GameObject bullet;
    //外部决定到底有几个发射位置
    public Transform[] shootPos;
    //武器拥有者
    public TankBaseObj fatherObj;
    public void SetFather(TankBaseObj obj)
    {
        fatherObj = obj;
    }
    public void Fire()
    {
        for (int i = 0; i < shootPos.Length; i++)
        {
            if (Time.timeScale == 0)
            {
                return;
            }
            GameObject obj = Instantiate(bullet, shootPos[i].position, shootPos[i].rotation);
            //控制子弹做什么
            BulletObj bulletObj= obj.GetComponent<BulletObj>();
            bulletObj.SetFather(fatherObj);
        }
    }
}
