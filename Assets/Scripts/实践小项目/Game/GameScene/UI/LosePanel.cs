using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : BasePanel<LosePanel>
{
    public CustomGUIButton btnReturnMenu;
    public CustomGUIButton btnRestart;
    // Start is called before the first frame update
    void Start()
    {
        btnRestart.clickEvent += () =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("GameScene " + GameDataManager.Instance.levelIndex);
        };
        btnReturnMenu.clickEvent += () =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("BeginScene");
        };
        HideMe();
    }

    
}
