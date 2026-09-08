using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPanel : BasePanel<LevelPanel>
{
    public List<CustomGUIButton> levelButtons;
    public CustomGUIButton btnClose;
    // Start is called before the first frame update
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
                GameDataManager.Instance.levelIndex = index+1;
                SceneManager.LoadScene("GameScene " + (index + 1));

            };
        }
        HideMe();
    }


}
