using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel :BasePanel<QuitPanel>
{
    public CustomGUIButton btnClose;
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnContinue;
    void Start()
    {
        btnClose.clickEvent += () =>
        {
            HideMe();

        };
        btnQuit.clickEvent += () =>
        {
            SceneManager.LoadScene("BeginScene");
        };
        btnContinue.clickEvent += () =>
        {
            HideMe();
        };
        HideMe();
    }
    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }
    public override void ShowMe()
    {
        base.ShowMe();
        Time.timeScale = 0;
    }
}
