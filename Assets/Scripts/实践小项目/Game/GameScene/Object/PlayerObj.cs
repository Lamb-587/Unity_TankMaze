using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : TankBaseObj
{
    public WeaponObj nowWeapon;
    public Transform weaponPos;
    public CharacterController cc;
    private float v;
    public List<BuffInfo> buffList = new List<BuffInfo>();
    public float buffDuration = 60f;
    public void AddBuff(E_PropType type,int value)
    {
        //同类buff已存在，只刷新时间，不叠加数值
        BuffInfo exist=buffList.Find(b=>b.type==type);
        if(exist!=null)
        {
            exist.remainingTime = exist.totalTime;
            return;
        }
        //立即生效
        if (type == E_PropType.Atk) atk += value;
        else if (type == E_PropType.Def) def += value;
        else return;
        buffList.Add(new BuffInfo { type=type, value = value ,totalTime=buffDuration,remainingTime=buffDuration});
    }
    public override void Fire()
    {
        if (nowWeapon != null)
        {
            nowWeapon.Fire();
        }
    }

    
    public override void Dead()
    {
        //玩家坦克，复写的死亡函数不可以把自己销毁，因为我们的主摄像机在玩家物体子对象下面，
        //如果销毁，会导致主摄像机被销毁，可能会出错，所以我们这里只是暂停游戏，然后弹出失败界面
        //base.Dead();
        Time.timeScale = 0;
        LosePanel.Instance.ShowMe();
    }
    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //更新主面板血条
        GamePanel.Instance.UpdateHP(this.maxHp, this.hp);
    }
    // Update is called once per frame
    void Update()
    {
        //1,WS键，控制 前进后退
        //知识点：1，transform 位移
        //2,input 轴向输入检测
        //this.transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * moveSpeed * Time.deltaTime);
        v = Input.GetAxis("Vertical");

        // 注意：CharacterController.Move 需要每帧都调用
        Vector3 move = transform.forward * v * moveSpeed * Time.deltaTime;
        cc.Move(move);  // 会被墙自动挡住，不会穿
        //身子旋转
        this.transform.Rotate(Input.GetAxis("Horizontal")*Vector3.up * roundSpeed * Time.deltaTime);
        //鼠标左右旋转
        tankHead.transform.Rotate(Input.GetAxis("Mouse X")*Vector3.up*headRoundSpeed * Time.deltaTime);
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        //从后往前遍历，方便删除
        for(int i = buffList.Count - 1; i >= 0; i--)
        {
            buffList[i].remainingTime-= Time.deltaTime;
            if (buffList[i].remainingTime <= 0)
            {
                //到期回退数值
                if (buffList[i].type == E_PropType.Atk) atk -= buffList[i].value;
                else if (buffList[i].type==E_PropType.Def) def -= buffList[i].value;
                buffList.RemoveAt(i);
            }
        }
    }
    public void ChangeWeapon(GameObject weapon)
    {
        if(nowWeapon!=null)
        {
            Destroy(nowWeapon.gameObject);
            nowWeapon=null;
        }
        GameObject weaponObj = Instantiate(weapon, weaponPos, false);
        nowWeapon=weaponObj.GetComponent<WeaponObj>();
        nowWeapon.SetFather(this);
    }
}
