using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardInfo :MonoBehaviour
{
    public E_PropType type;
    public int value;
    public float remainTime;
    public float totalTime;

    public RewardInfo(E_PropType type, int value,float duration)
    {
        this.type = type;
        this.value = value;
        this.remainTime = duration;
        this.totalTime = duration;
    }
}
