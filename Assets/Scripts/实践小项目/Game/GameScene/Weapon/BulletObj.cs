using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    public float moveSpeed=50f;
    public TankBaseObj fatherObj;
    public GameObject effObj;
    public void SetFather(TankBaseObj obj)
    {
        fatherObj = obj;
    }
    

    // Update is called once per frame
    void Update()
    {
        this.transform .Translate(Vector3.forward* moveSpeed*Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == null || other.gameObject == null)
        {
            return;
        }
        // 2. 【关键】检查 fatherObj，发射者可能已经被销毁了
        if (fatherObj == null || fatherObj.gameObject == null)
        {
            Destroy(this.gameObject); // 发射者都没了，子弹直接销毁
            return;
        }
        if (other.tag == "Cube" || other.tag == "Player" && fatherObj.tag == "Monster" || other.CompareTag("Monster") && fatherObj.CompareTag("Player")) 
        {
            TankBaseObj obj =other.GetComponent<TankBaseObj>();
            if (obj != null)
            {
                obj.Wound(this.fatherObj);
            }
            //当子弹销毁时，创建一个爆炸特效
            if(effObj != null)
            {
                GameObject eff=Instantiate(effObj,this.transform.position,this.transform.rotation);
                AudioSource audioSource = eff.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.volume = GameDataManager.Instance.musicData.soundValue;
                    audioSource.mute = !GameDataManager.Instance.musicData.isOpenSound;
                }
                
            }
            Destroy(this.gameObject);
        }
    }
}
