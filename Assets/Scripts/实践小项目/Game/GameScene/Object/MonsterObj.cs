using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObj : TankBaseObj
{
    public Transform[] randomPos;
    private Transform targetPos;
    //1,地方坦克来回移动
    //2，敌方坦克盯住玩家进行开火
    //盯向的玩家
    public Transform targetPlayer;
    //3，当玩家进入攻击范围后开火
    public float fireDis=10;
    private float nowTime = 0;
    public float fireOffsetTime = 1.5f;
    //开火逻辑：需要有子弹，需要有发射位置，记得子弹生成后需要设定它的父对象
    public GameObject bulletObj;
    public Transform[] firePos;
    //底图
    public Texture maxHpTex;
    public Texture hpTex;
    private Rect maxHpRect;
    private Rect hpRect;
    //显示时间
    public float showTime = 5;
    private float tempTime ;
    public override void Fire()
    {
        for(int i=0;i<firePos.Length;i++)
        {
            GameObject obj = Instantiate(bulletObj, firePos[i].position, firePos[i].rotation);
            BulletObj bullet = obj.GetComponent<BulletObj>();
            if(bullet != null)
            {
                bullet.SetFather(this);
            }
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomPos();
        tempTime = showTime;
    }

    // Update is called once per frame
    void Update()
    {
        tempTime += Time.deltaTime;
        nowTime += Time.deltaTime;
        this.transform.LookAt(targetPos);
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (Vector3.Distance(this.transform.position, targetPos.position) < 0.1f)
        {
            RandomPos();
        }
        if(targetPlayer != null)
        {
            this.tankHead.transform.LookAt(targetPlayer);
            if(Vector3.Distance(this.transform.position,targetPlayer.position)<= fireDis)
            {
                if(nowTime>=fireOffsetTime)
                {

                    Fire();
                    nowTime = 0;
                }
                
            }

        }
        

    }
    public void RandomPos()
    {
        if(randomPos.Length==0)
        {
            return;
        }
        targetPos = randomPos[Random.Range(0,randomPos.Length)];
    }
    public override void Dead()
    {
        base.Dead();
        GamePanel.Instance.AddScore(10);
    }
    private void OnGUI()
    {
        
        if(tempTime<=showTime)
        {
            //画图，画血条，1，把怪物当前位置转换为屏幕位置，
            //摄像机里面提供了API，可以
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            //2，屏幕位置转为GUI位置
            screenPos.y = Screen.height - screenPos.y;
            //然后再绘制
            if (screenPos.z <= 0) return;  // 在摄像机后面就不画
            //先画底图
            maxHpRect.x = screenPos.x - maxHpRect.width / 2;
            maxHpRect.y = screenPos.y - maxHpRect.height / 2;
            maxHpRect.width = 300 / (screenPos.z / 10);
            maxHpRect.height = 25 / (screenPos.z / 10);
            GUI.DrawTexture(maxHpRect, maxHpTex);
            //在画血条
            hpRect.x = maxHpRect.x;
            hpRect.y = maxHpRect.y;
            hpRect.width = (float)hp / maxHp * maxHpRect.width;
            hpRect.height = maxHpRect.height;
            GUI.DrawTexture(hpRect, hpTex);
            //*************************************************************
            //if (screenPos.z <= 0) return;  // 在摄像机后面就不画
            ////先画底图
            //maxHpRect.x = screenPos.x - 150;
            //maxHpRect.y = screenPos.y - 150;
            //maxHpRect.width = 300 ;
            //maxHpRect.height = 25 ;
            //GUI.DrawTexture(maxHpRect, maxHpTex);
            ////在画血条
            //hpRect.x = maxHpRect.x;
            //hpRect.y = maxHpRect.y;
            //hpRect.width = (float)hp / maxHp * maxHpRect.width;
            //hpRect.height = maxHpRect.height;
            //GUI.DrawTexture(hpRect, hpTex);
        }
        
    }
    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        tempTime = 0;
    }
}
