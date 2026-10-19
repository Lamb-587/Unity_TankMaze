using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    public PlayerObj player;
    public GameObject atkIcon;
    public Image atkFill;
    public GameObject defIcon;
    public Image defFill;
    public Text atkText;
    public Text defText;

    private BuffInfo atkBuff;
    private BuffInfo defBuff;

    private void Awake()
    {
        if(player==null) player = FindObjectOfType<PlayerObj>();
    }
    // Update is called once per frame
    void Update()
    {
        atkBuff=RefreshSlot(E_PropType.Atk,atkBuff,atkIcon,atkFill,atkText);
        defBuff=RefreshSlot(E_PropType.Def,defBuff,defIcon,defFill,defText);
    }
    private BuffInfo RefreshSlot(E_PropType type,BuffInfo cache,GameObject icon,Image fill,Text textRemainingTime)
    {
        //缓存失败（不存在或已经过期）就重新找
        if(cache== null || cache.remainingTime <= 0)
        {
            cache=player.buffList.Find(x => x.type == type);
            icon.SetActive(cache!=null);
        }
        if(cache!=null)
        {
            fill.fillAmount = cache.remainingTime / cache.totalTime;
            textRemainingTime.text = cache.remainingTime.ToString("F2");
            
        }
        return cache;
    }
}
