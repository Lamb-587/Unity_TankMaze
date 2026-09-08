using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRankPanel : BasePanel<LevelRankPanel>
{
    public List<CustomGUIButton> levelButtons;
    public CustomGUIButton btnClose;
    void Start()
    {
        //处理点击关闭按钮后的逻辑
        btnClose.clickEvent += () =>
        {
            //关闭按钮的逻辑
            HideMe();
            BeginPanel.Instance.ShowMe();
        };
        for (int i = 0; i < levelButtons.Count; i++)
        {
            int index = i;  // 关键：用局部变量保存当前 i 的值

            levelButtons[i].clickEvent += () =>
            {
                
                GameDataManager.Instance.LoadRankData(index + 1);
                RankPanel.Instance.ShowMe();
                HideMe();

            };
        }
        HideMe();
    }

    
}
