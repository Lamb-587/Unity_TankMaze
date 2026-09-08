using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if (other.CompareTag("Player"))
            {
                Time.timeScale = 0;
                
                
                //首先进行判断，如果是没有通关过教学关卡，那么这次将标识isPassTeachingGame 改为true，不能让他输入用户名，
                //因为教学关卡不记录成绩
                //所以，不要出现win的界面，让他直接选择关卡

                
                if (PlayerPrefs.GetInt("PassedTeachingGame")==0)
                {
                    //赢了，但是没过教学，改标识，进入正式的选关
                    BeginPanel.Instance.MarkTeachingAsPassed();
                    LevelPanel.Instance.ShowMe();
                }
                else
                {

                    //过过教学了，这是某一关通关了，出胜利界面
                    WinPanel.Instance.ShowMe();
                }
                
            }
        }
    }
}
