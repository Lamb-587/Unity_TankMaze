using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakCube : MonoBehaviour
{
    public GameObject[] rewardObjects;
    public GameObject effObj;
    private void OnTriggerEnter(Collider other)
    {
        //1，打到自己的子弹应该销毁
        //第一步不用在这里写逻辑了，只需要把箱子的tag,改成cube,子弹销毁的逻辑在bullet脚本=已经写好了
        //2，子弹打到自己，我自己要破碎，并且随机出现奖励
        GameObject eff=Instantiate(effObj,this.transform.position,this.transform.rotation);
        AudioSource audioSource=eff.GetComponent<AudioSource>();
        audioSource.volume = GameDataManager.Instance.musicData.soundValue;
        audioSource.mute = !GameDataManager.Instance.musicData.isOpenSound;
        int rangeint = Random.Range(0, 100);
        if (rangeint < 50)
        {
            //随机创建一个奖励预设体
            rangeint = Random.Range(0, 5);
            Instantiate(rewardObjects[rangeint],this.transform.position,this.transform.rotation);
        }
        Destroy(this.gameObject);
    }
}
