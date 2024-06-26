using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEditor.Rendering;

public class CameraController : MonoBehaviour
{

    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float maxShakeTime = 0.5f;

    [SerializeField] private GameObject playerObject;
    [SerializeField] private float movingDuration = 0.5f;

    void Awake()
    {
        //MuzzleController.OnShooting += ShakeCamera;
        Debug.Log("=====================> CIAO\n");

    }

    private void OnDestroy()
    {
        //MuzzleController.OnShooting -= ShakeCamera;
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

    void FixedUpdate()
    {
        //FollowPlayer();
    }

    void ShakeCamera()
    {
       StartCoroutine(ShakeCameraNumerator()); 
    }

    void FollowPlayer()
    {
        transform.DOMove(playerObject.transform.position,movingDuration);
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
