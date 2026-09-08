using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginPanel :BasePanel<BeginPanel>
{
    public CustomGUIButton btnBegin;
    public CustomGUIButton btnSet;
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnRank;
    //改
    // 不要直接在这里存状态，改为从 PlayerPrefs 读取
    private bool IsPassedTeachingGame
    {
        get => PlayerPrefs.GetInt("PassedTeachingGame", 0) == 1;
        set => PlayerPrefs.SetInt("PassedTeachingGame", value ? 1 : 0);
    }
    // 在新手教程通关的地方调用这个方法
    public void MarkTeachingAsPassed()
    {
        PlayerPrefs.SetInt("PassedTeachingGame", 1);
        PlayerPrefs.Save();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        
        btnBegin.clickEvent += () =>
        {
            if (IsPassedTeachingGame == false)
            {
                //点击开始游戏之后的相关逻辑
                //先进新手教程
                SceneManager.LoadScene("GameScene");
            }
            else
            {
                //进入选择关卡界面
                LevelPanel.Instance.ShowMe();
            }
            HideMe();
            
        };
        btnSet.clickEvent += () =>
        {
            //加载设置游戏界面，同时隐藏自己
            HideMe();
            SettingPanel.Instance.ShowMe();

        };
        btnQuit.clickEvent += () =>
        {
            Application.Quit();
        };
        btnRank.clickEvent += () =>
        {
            LevelRankPanel.Instance.ShowMe();
            HideMe();
        };
    }
}
