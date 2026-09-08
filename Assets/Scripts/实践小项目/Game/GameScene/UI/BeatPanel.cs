using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeatPanel : BasePanel<BeatPanel>
{
    public CustomGUIButton btnReturnMenu;
    // Start is called before the first frame update
    void Start()
    {
        btnReturnMenu.clickEvent += () =>
        {
            SceneManager.LoadScene("BeginScene");
        };
        HideMe();
    }

    
}
