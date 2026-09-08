using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager 
{
    //依旧单例模式
    private static GameDataManager instance=new GameDataManager();
    public static GameDataManager Instance { get { return instance; } }
    //音乐相关数据对象
    public  MusicData musicData;
    //排行榜数据对象
    public Dictionary<int, RankList> rankDict=new Dictionary<int, RankList>();
    //public RankList rankData;
    //改，新增公共的关卡标识：
    public int levelIndex;
    private GameDataManager() {
        musicData = PlayerPrefsDataMgr.Instance.LoadData(typeof(MusicData), "music") as MusicData;
        
        if (!musicData.notFirst)
        {
            musicData.notFirst = true;
            musicData.musicValue = 1f;
            musicData.soundValue = 1f;
            musicData.isOpenMusic = true;
            musicData.isOpenSound = true;
            PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
        }

    }
    //提供给外部API
    /// <summary>
    /// 加载指定关卡的排行榜（没有就自动创建空的）
    /// </summary>
    public void LoadRankData(int level)
    {
        levelIndex = level;

        // 如果已经加载过，直接返回
        if (rankDict.ContainsKey(level))
            return;

        // 从存档读取
        RankList data = PlayerPrefsDataMgr.Instance.LoadData(typeof(RankList), "Rank" + level) as RankList;

        // 如果该关卡从来没玩过，LoadData 可能返回一个空对象（字段全是默认值）
        // 确保 list 不是 null
        if (data.list == null)
            data.list = new List<RankInfo>();

        rankDict[level] = data;
    }
    /// <summary>
    /// 获取当前关卡的排行榜数据（自动加载）
    /// </summary>
    public RankList GetCurrentRankData()
    {
        if (!rankDict.ContainsKey(levelIndex))
            LoadRankData(levelIndex);

        return rankDict[levelIndex];
    }
    //提供一个在排行榜中添加数据的方法
    public void AddRankInfo(string name,int score,float time)
    {
        RankList current = GetCurrentRankData();

        current.list.Add(new RankInfo(name, score, time));
        //排序,按时间排序
        current.list.Sort((a, b) => a.time < b.time ? -1 : 1);
        //排序过后，移除10条之外的数据
        //从尾部往前遍历 ，移除每一条
        for (int i = current.list.Count - 1; i >= 10; i--)
        {
            current.list.RemoveAt(i);
        }

        PlayerPrefsDataMgr.Instance.SaveData(current, "Rank" + levelIndex);
    }

    public void OpenOrCloseMusic(bool isOpen)
    {
        musicData.isOpenMusic= isOpen;
        BKMusic.Instance.OpenMusic(isOpen);
        //接着就存好数据
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }
    public void OpenOrCloseSound(bool isOpen)
    {
        musicData.isOpenSound = isOpen;
        //接着就存好数据
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }
    public void ChangeMusicValue(float value)
    {
        musicData.musicValue = value;
        BKMusic.Instance.ChangeValue(value); 
        //接着就存好数据,
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }
    public void ChangeSoundValue(float value)
    {
        musicData.soundValue = value;
        //接着就存好数据
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "music");
    }

}
