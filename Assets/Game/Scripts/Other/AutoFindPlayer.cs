using UnityEngine;
using Unity.Cinemachine;

public class AutoFindPlayer : MonoBehaviour
{
    private void Awake()
    {
        // Tìm Player dựa trên Tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            CinemachineCamera vCam = GetComponent<CinemachineCamera>();
            if (vCam != null)
            {
                vCam.Follow = player.transform;
                vCam.LookAt = player.transform;
            }
        }
    }
}
