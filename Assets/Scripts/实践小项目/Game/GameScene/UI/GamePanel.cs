using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel :BasePanel<GamePanel>
{
    public CustomGUILabel labScore;
    public CustomGUILabel labTime;
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnSetting;
    public CustomGUITexture texHP;
    public CustomGUITexture texMaxHp;

    private int time;
    //最大血量与最大血量血条宽度的关系
    private float relation_maxHp;
    //血条控件的宽
    public float hpw = 550;
    [HideInInspector]
    public int nowScore = 0;
    [HideInInspector]
    public  float nowTime=0;

    void Start()
    {
        UpdateHP(100, 100);
        relation_maxHp = hpw / 100f;


        btnQuit.clickEvent += () =>
        {
            //点击退出按钮的逻辑：返回上一级
            //并且最好加一个确认界面，防止误操作
            QuitPanel.Instance.ShowMe();

        };
        btnSetting.clickEvent += () =>
        {
            //点击设置按钮后的逻辑：展示设置界面
            SettingPanel.Instance.ShowMe();


        };

    }

    // Update is called once per frame
    void Update()
    {
        nowTime += Time.deltaTime;
        time = (int)nowTime;
        labTime.content.text = "";
        //得到几个小时,大于0说明有小时，那么我们需要打印小时
        if (time / 3600 > 0)
        {
            labTime.content.text += time / 3600 + "时";

        }
        if (time % 3600 / 60 > 0 || labTime.content.text != "")
        {
            labTime.content.text += time % 3600 / 60 + "分";
        }
        labTime.content.text += time % 60 + "秒";
    }
    public void UpdateHP(int maxHP,int HP)
    {
        texHP.guiPos.width = (float)HP / maxHP * hpw;
    }
    public void UpdateMaxHp(int maxHp)
    {
        if (maxHp > 250)
        {
            return;
        }
        texMaxHp.guiPos.width =  maxHp*relation_maxHp;
        
    }
    public void AddScore(int score)
    {
        nowScore += score;
        labScore.content.text=nowScore.ToString();
    }
}
