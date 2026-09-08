using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankBaseObj : MonoBehaviour
{

    public int atk;
    public int def;
    public int maxHp;
    public int hp;
    //tank的头部
    public GameObject tankHead;
    //旋转速度，移动速度相关
    public float moveSpeed = 10f;
    public float roundSpeed = 100f;
    public float headRoundSpeed = 100f;
    //死亡特效 关联对应预设体，死亡的时候动态创建出来 设置位置即可
    public GameObject deadEff;

    public abstract void Fire();
    public virtual void Wound(TankBaseObj other)
    {
        int dmg = other.atk - def;
        if(dmg<=0)
        {
            return;
        }
        this.hp-=dmg;
        //判断 如果血量《=0了，就应该调用死亡函数
        if (hp <= 0)
        {
            this.hp = 0;
            Dead();
        }
    }
    public virtual void Dead()
    {
        Destroy(this.gameObject);
        if (deadEff != null )
        {
            GameObject effObj=Instantiate(deadEff,this.transform.position,this.transform.rotation);
            //由于该特效对象身上直接关联了音效，所以我们顺便把音效播放相关也控制了
            AudioSource audioSource= effObj.GetComponent<AudioSource>();
            //根据音乐数据 设置音效的大小和开关
            audioSource.volume = GameDataManager.Instance.musicData.soundValue;
            audioSource.mute = !GameDataManager.Instance.musicData.isOpenSound;
            //避免没有  勾选Play  on Awake
            audioSource.Play();
        }
    }
}
