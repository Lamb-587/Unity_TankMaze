using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float H = 17;
    public Transform targetPlayer;
    private Vector3 pos;
    private void LateUpdate()
    {
         if (targetPlayer != null)
        {
            pos.x = targetPlayer.position.x;
            pos.z = targetPlayer.position.z;
            pos.y = H;
            this.transform.position = pos;
        }
    }
}
