using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    [Header("玩家引用")]
    [SerializeField] private PlayerObj player;

    private List<RewardInfo> activeBuffs = new List<RewardInfo>();
    //供外部添加
    public List<GameObject> listBuffs = new List<GameObject>();
    //单例模式
    public static RewardPanel Instance{get; private set;}
    
    private void Awake()
    {
        // 单例初始化
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // 如果编辑器没拖玩家引用，运行时自动找
        if (player == null)
        {
            player = FindObjectOfType<PlayerObj>();
        }

    }
    
    void Update()
    {
        //由于涉及到移除列表内容，所以我们需要倒着来遍历
        for (int i = activeBuffs.Count-1; i >-1 ; i--)
        {
            RewardInfo info = activeBuffs[i];
            //1,减少剩余时间
            info.remainTime-= Time.deltaTime;
            //2，更新UI图标的填充量（从1到0，逆时针消减）
            UpdateIconFill(info);
            //3,判断，如果剩余时间<0，清除buffUI，扣回属性值
            if(info.remainTime <= 0) {
                //扣回属性
                RemoveValueFromPlayer(info.type, info.value);
                //销毁UI图标
                DestroyUI(info.type);
                //从列表移除
                activeBuffs.RemoveAt(i);
            }
        }
    }

    private void DestroyUI(E_PropType type)
    {
        switch (type)
        {
            case E_PropType.Atk:
                GameObject obj = GameObject.Find("UI_ATK");
                if (obj != null)
                {
                    Destroy(obj);
                }
                break;
            case E_PropType.Def:
                obj = GameObject.Find("UI_DEF");
                if (obj != null)
                {
                    Destroy(obj);
                }
                break;
        }
    }

    private void RemoveValueFromPlayer(E_PropType type, int value)
    {
        if(player == null) { return; }
        switch(type)
        {
            case E_PropType.Atk:
                player.atk-= value; break;
                case E_PropType.Def: player.def-= value; break;
        }
    }

    private void UpdateIconFill(RewardInfo info)
    {
        //修改挂载了RewardInfo脚本的对象的子对象的image组件
        //里的fill amount参数
        Transform child = this.transform.Find("bkground");
        if (child != null)
        {
            Image image=child.GetComponent<Image>();
            if (image == null) return;
            float ratio = info.totalTime > 0f ? info.remainTime / info.totalTime : 0f;
            image.fillAmount = Mathf.Clamp01(ratio);
        }
    }
    /// <summary>
    /// 在活跃列表里找同类型的Buff，找到返回引用，找不到返回null。
    /// </summary>
    private RewardInfo FindRewardInfoByType(E_PropType type)
    {
        for(int i = 0;i<activeBuffs.Count;i++)
        {
            if (activeBuffs[i].type == type)
                return activeBuffs[i];
        }
        return null;
    }
    private void AddValueToPlayer(E_PropType type, int value)
    {
        if(player != null)
        {
            switch (type)
            {
                case E_PropType.Atk:
                    player.atk+= value; break;
                case E_PropType.Def:
                    player.def+= value; break;
            }
        }
    }
    //公共方法：
    public void AddReward(E_PropType type,int value,float duration)
    {
        //1,查找是否有同类buff
        RewardInfo existingInfo=FindRewardInfoByType(type);
        if (existingInfo != null)
        {
            //同类已经存在：叠加数值，刷新时间
            existingInfo.value += value;
            existingInfo.remainTime = duration;
            existingInfo.totalTime = duration;
            //数值叠加到玩家身上
            AddValueToPlayer(type, value);
            //刷新图标填充（让进度条重新变满）
            UpdateIconFill(existingInfo);
        }
        else
        {
            //不存在：创建新Buff
            //实例化UI图标
            GameObject iconObj = null;
            switch (type)
            {
                case E_PropType.Atk:
                    iconObj = Instantiate(listBuffs[0],this.transform);
                    break;
                    case E_PropType.Def:
                    iconObj = Instantiate(listBuffs[1],this.transform);
                    break;
            }
            //创建数据对象
            RewardInfo newReward=new RewardInfo(type,value,duration);
            activeBuffs.Add(newReward);
            //数值加到玩家身上
            AddValueToPlayer(type, value);
            //进度条开始走
            UpdateIconFill(newReward);
        }
    }
}
