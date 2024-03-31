using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float maxShakeTime = 0.5f;

    void Awake()
    {
        virtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        virtualCamera.enabled = false;
        MuzzleController.OnShooting += ShakeCamera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShakeCamera()
    {
        StartCoroutine(ShakeCameraEnumerator());
    }

    IEnumerator ShakeCameraEnumerator()
    {
        /*CinemachineVirtualCamera virtualCamera;
        virtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        virtualCamera.enabled = false;*/

        CinemachineVirtualCamera virtualCameraEnumerator;
        virtualCameraEnumerator = gameObject.GetComponent<CinemachineVirtualCamera>();

        virtualCameraEnumerator.enabled = true;
        yield return new WaitForSeconds(maxShakeTime);
        virtualCameraEnumerator.enabled = false;
        yield return null;
    }



}
