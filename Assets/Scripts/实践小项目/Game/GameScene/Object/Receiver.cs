using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    [SerializeField]
    private GameObject targetPanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && targetPanel != null)
            targetPanel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && targetPanel != null)
            targetPanel.SetActive(false );
    }
}
