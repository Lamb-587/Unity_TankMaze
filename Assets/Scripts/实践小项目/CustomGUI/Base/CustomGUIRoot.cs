using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CustomGUIRoot : MonoBehaviour
{
    //用于存储 子对象 所有GUI控件的容器
    private CustomGUIControl[] allControls;
    //用于存储场景中所有激活的AI坦克的容器
    // CustomGUIRoot里加一个登记表
    public static List<MonsterObj> monsters = new List<MonsterObj>();
    void Start()
    {
        allControls = this.GetComponentsInChildren<CustomGUIControl>(true);
    }

    //在这同一绘制子对象控件的内容
    private void OnGUI()
    {
        //通过每一次绘制之前 得到所有子对象控件的 父类脚本
        //这句代码 浪费性能 因为每次 gui都会来获取所有的 控件对应的脚本
        //编辑状态下 才会一直执行
        if (!Application.isPlaying)
        {
            allControls = this.GetComponentsInChildren<CustomGUIControl>();
        }
        // OnGUI的绘制循环改成两段：
        foreach (var m in monsters) m.DrawHpBar();              // ① 先画血条
        //遍历每一个控件 让其 执行绘制
        for (int i = 0; i < allControls.Length; i++)              // ② 再画面板（盖在上面）
        {
            if (allControls[i].gameObject.activeInHierarchy)  // 冻住的数组+每帧活性检查
                allControls[i].DrawGUI();
        }
    }
}
