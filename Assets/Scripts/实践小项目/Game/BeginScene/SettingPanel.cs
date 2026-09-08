using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel :BasePanel<SettingPanel>
{
    public CustomGUIButton btnClose;
    public CustomGUIToggle togMusic;
    public CustomGUIToggle togSound;
    public CustomGUISlider sliderMusic;
    public CustomGUISlider sliderSound;
    public override void ShowMe()
    {
        base.ShowMe();
        Time.timeScale = 0;
        //每次显示面板时 ，顺便把面板上的内容也更新
        UpdatePanelData();
    }
    public void UpdatePanelData()
    {
        //我们面板上的信息都是根据音效数据更新
        MusicData data = GameDataManager.Instance.musicData;
        //设置面板内容
        sliderMusic.nowValue = data.musicValue;
        sliderSound.nowValue = data.soundValue;
        togMusic.isSel = data.isOpenMusic;
        togSound.isSel = data.isOpenSound;
    }
    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }
    void Start()
    {

        btnClose.clickEvent += () =>
        {
            //关闭按钮的逻辑
            HideMe();
            if (SceneManager.GetActiveScene().name == "BeginScene")
            {
                BeginPanel.Instance.ShowMe();
            }
            
        };
        togMusic.changeValue += (value) =>
        {
            //音乐开关
            GameDataManager.Instance.OpenOrCloseMusic(value);
        };
        togSound.changeValue += (value) =>
        {
            //音效开关
            GameDataManager.Instance.OpenOrCloseSound(value);
        };
        sliderMusic.changeValue += (value) =>
        {
            //音乐大小
            GameDataManager.Instance.ChangeMusicValue(value);
        };
        sliderSound.changeValue += (value) =>
        {
            //音效大小
            GameDataManager.Instance.ChangeSoundValue(value);
        };
        //当所有的事件赋值完毕后，我们开始把自己隐藏了，毕竟不能一开始就是设置界面嘛
        HideMe ();
    }

}
