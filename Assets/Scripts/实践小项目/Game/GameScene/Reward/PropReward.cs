using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PropType
{
    Atk,
    Def,
    MaxHp,
    Hp,
}
public class PropReward : MonoBehaviour
{
    public E_PropType type = E_PropType.Def;
    public GameObject getEff;
    public int changeValue=10;
    /// <summary>
    /// 时限Buff的持续时间（秒）。
    /// 只对Atk和Def生效，MaxHp和Hp是永久的不走这个。
    /// </summary>
    [Header("时限Buff配置")]
    public float buffDuration = 60f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            PlayerObj player=other.gameObject.GetComponent<PlayerObj>();
            switch (type)
            {
                case E_PropType.Atk:
                    RewardPanel.Instance.AddReward(E_PropType.Atk, changeValue,buffDuration);
                    break;
                    case E_PropType.Def:
                    RewardPanel.Instance.AddReward(E_PropType.Def, changeValue, buffDuration);
                    break;
                    case E_PropType.MaxHp:
                    //GamePanel.Instance.hpw += GamePanel.Instance.hpw / (float)player.maxHp * changeValue;
                    //失败了，不是代码逻辑错了，是以为当时写的时候并不会更新底图的宽，只会更改hp的宽
                    //如果我们要实现吃了加最大血量的buff,后，增加黑条（底图）的长度，我们需要，接收一下MaxHp再写一个updateMaxHp方法
                    
                    player.maxHp += changeValue;
                    
                    //更新血条
                    GamePanel.Instance.UpdateMaxHp(player.maxHp);
                    GamePanel.Instance.UpdateHP(player.maxHp, player.hp);
                    break;
                    case E_PropType.Hp:
                    player.hp += changeValue;
                    if(player.hp >player.maxHp)
                    {
                        player.hp = player.maxHp;
                    }
                    //更新血条
                    GamePanel.Instance.UpdateHP(player.maxHp, player.hp);
                    break;

            }
            GameObject obj = Instantiate(getEff, this.transform.position, transform.rotation);
            AudioSource audioSource = obj.GetComponent<AudioSource>();
            audioSource.volume = GameDataManager.Instance.musicData.soundValue;
            audioSource.mute = !GameDataManager.Instance.musicData.isOpenSound;
            audioSource.Play();
            //获取到自己后，移除自己
            Destroy(this.gameObject);
        }
    }
}
