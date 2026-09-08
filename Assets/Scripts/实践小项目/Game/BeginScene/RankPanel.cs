using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel :BasePanel<RankPanel>
{
    public CustomGUIButton btnClose;
    //改
    public CustomGUILabel labLevel; 

    private List<CustomGUILabel> labelPlayerName = new List<CustomGUILabel>();
    private List<CustomGUILabel> labelScore = new List<CustomGUILabel>();
    private List<CustomGUILabel> labelPassTime = new List<CustomGUILabel>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 10; i++)
        {
            //额外知识点：找子对象的子对象可以通过斜杠来区分父子关系
            
            labelPlayerName.Add(this.transform.Find("PN/PlayerName" + " (" + i + ")").GetComponent<CustomGUILabel>());
            labelScore.Add(this.transform.Find("Sc/Score" + " (" + i + ")").GetComponent<CustomGUILabel>());
            labelPassTime.Add(this.transform.Find("PT/PassTime" + " (" + i + ")").GetComponent<CustomGUILabel>());
        }
        //处理点击关闭按钮后的逻辑
        btnClose.clickEvent += () =>
        {
            //关闭按钮的逻辑
            HideMe();
            LevelRankPanel.Instance.ShowMe();
        };
        //测试数据
        //GameDataManager.Instance.AddRankInfo("cdadad", 100, 2532);
        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        UpdateRankData();
    }
    public void UpdateRankData()
    {
        //更新界面的关卡数，只是一个提示作用,提示这个界面的关卡数
        labLevel.content.text = GameDataManager.Instance.levelIndex.ToString();
        //更新数据相关逻辑
        //获取GameDataMgr中的排行榜列表 用于在这里更新
        //得数据
        List<RankInfo> list = GameDataManager.Instance.GetCurrentRankData().list;
        
        // ① 先把所有面板项重置为"虚伪以待"
        for (int i = 0; i < 10; i++)
        {
            labelPlayerName[i].content.text = "虚伪以待";
            labelScore[i].content.text = "0";
            labelPassTime[i].content.text = "0";
        }
        //②根据列表更新面板数据
        for (int i = 0; i < list.Count; i++)
        {
            //name
            labelPlayerName[i].content.text = list[i].name;
            //score
            labelScore[i].content.text = list[i].score.ToString();
            //时间 ，存储得时间单位是s,甚至是float,但是我们这里显示得用时分秒
            int time = (int)list[i].time;
            labelPassTime[i].content.text = "";
            //得到几个小时,大于0说明有小时，那么我们需要打印小时
            if(time / 3600 > 0)
            {
                labelPassTime[i].content.text += time / 3600 + "时";
                
            }
            if (time % 3600 / 60 > 0 || labelPassTime[i].content.text != "")
            {
                labelPassTime[i].content.text += time % 3600 / 60 + "分";
            }
            labelPassTime[i].content.text += time % 60 + "秒";
        }
    }


}
