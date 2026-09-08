using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel :BasePanel<WinPanel>
{
    public CustomGUIButton btnSure;
    public CustomGUIButton btnNext;
    public CustomGUIInput inputUserName;
    
    // Start is called before the first frame update
    
    void Start()
    {
        //endPoint的碰撞检测后就会暂停时间，所以这里不用写，只需要准备什么时候重新开始时间
        btnNext.clickEvent += () =>
        {
            //记录成绩，选择下一关
            GameDataManager.Instance.AddRankInfo(inputUserName.content.text, GamePanel.Instance.nowScore, GamePanel.Instance.nowTime);
            Time.timeScale = 1.0f;
            // 关卡数 +1
            GameDataManager.Instance.levelIndex++;
            if (SceneHelper.IsSceneInBuildSettings("GameScene " + GameDataManager.Instance.levelIndex))
            {
                GameDataManager.Instance.LoadRankData(GameDataManager.Instance.levelIndex);
                SceneManager.LoadScene("GameScene " + GameDataManager.Instance.levelIndex);
            }
            else
            {
                BeatPanel.Instance.ShowMe();
            }
            
        };
        btnSure.clickEvent += () =>
        {
            //记录成绩,返回主菜单
            GameDataManager.Instance.AddRankInfo(inputUserName.content.text, GamePanel.Instance.nowScore, GamePanel.Instance.nowTime);
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("BeginScene");
        };
        HideMe();
    }

    
}
