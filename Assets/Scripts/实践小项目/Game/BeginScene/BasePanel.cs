using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//为什么要加约束，因为后面要用到as ,只有类才可以AS，因为T可以是结构体还是别的什么
//我们不能把this (代表一个类嘛）as 成一个结构体，所以加了约束
public class BasePanel<T> : MonoBehaviour where T:class
{
    //私有的静态成员变量（用来声明）
    private static T instance;
    //公共的静态成员属性或者方法（用来获取
    public static T Instance => instance;
    private void Awake()
    {
        //在Awake中初始化的原因是 ，我们的面板脚本在场景上肯定只会挂在一次
        //那么我们可以在这个脚本的生命周期函数的Awake中
        //直接记录场景上唯一的这个脚本
        instance = this as T; 
    }
    
    public virtual void  ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    public virtual void HideMe()
    {
        this.gameObject.SetActive(false);
    }
}
