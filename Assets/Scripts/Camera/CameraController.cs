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
        MuzzleController.OnShooting += ShakeCamera;
    }

    private void OnDestroy()
    {
        MuzzleController.OnShooting -= ShakeCamera;
    }

    private void Start()
    {
        virtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        virtualCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShakeCamera()
    {
       StartCoroutine(ShakeCameraNumerator()); 
    }

    IEnumerator ShakeCameraNumerator()
    {
        CinemachineVirtualCamera cam = gameObject.GetComponent<CinemachineVirtualCamera>();
        if(cam != null)
        {
            //Debug.Log("CAMERA IS OK");
            cam.enabled = !cam.enabled;
            yield return new WaitForSeconds(maxShakeTime);
            cam.enabled = !cam.enabled;
            yield return null;
        }
        else
        {
            Debug.Log("CAMERA IS NULL");
            yield return null;
        }
    }

    



}
