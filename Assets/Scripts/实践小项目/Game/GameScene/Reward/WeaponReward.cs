using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReward : MonoBehaviour
{
    public GameObject[] weaponObj;
    public GameObject effObj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //让玩家切换武器
            int index =Random.Range(0, weaponObj.Length);
            //得到撞到的玩家身上挂载的  脚本 ，命令玩家对象调用切换武器的方法
            other.GetComponent<PlayerObj>().ChangeWeapon(weaponObj[index]);
            //更换完武器后播放吃到增益buff的声音
            GameObject obj=Instantiate(effObj,this.transform.position,transform.rotation);
            AudioSource audioSource=obj.GetComponent<AudioSource>();
            audioSource.volume = GameDataManager.Instance.musicData.soundValue;
            audioSource.mute = !GameDataManager.Instance.musicData.isOpenSound;
            audioSource.Play();
            //获取到自己后，移除自己
            Destroy(this.gameObject);
        }
    }
}
