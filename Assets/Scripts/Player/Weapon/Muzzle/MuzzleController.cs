using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MuzzleController : MonoBehaviour
{

    public static event Action OnShooting;

    [SerializeField] private GameObject projectile;
    private Rigidbody2D projectileBody;

    [SerializeField] private float maxShootingTimer = 0.5f;
    private float shootingTimer = 0f;

    private int maxPorjectileArray;
    [SerializeField] GameObject projetctileParent;
    GameObject[] projectilesArray;
    private int projectilesArrayIndex = 0;

    private Vector3 desiredRotationProjectile;

    [SerializeField] private AudioClip shootingAudio;

    void Start()
    {
        projectileBody = projectile.GetComponent<Rigidbody2D>();


        maxPorjectileArray = projetctileParent.transform.childCount;
        projectilesArray = new GameObject[maxPorjectileArray];
        //Debug.Log(maxPorjectileArray);
        for (int i = 0; i < maxPorjectileArray; ++i)
        {
            //Debug.Log("INDEX -> " + i);
            projectilesArray[i] = projetctileParent.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckShooting();
    }

    void CheckShooting()
    {
        if(shootingTimer < maxShootingTimer)
        {
            shootingTimer += Time.deltaTime;
        }

        if(Input.GetMouseButton(0) && shootingTimer>maxShootingTimer){

            OnShooting?.Invoke();

            ActivateProjectile();
        }

        if (Input.GetMouseButtonDown(0) && shootingTimer>maxShootingTimer)
        {
            OnShooting?.Invoke();

            ActivateProjectile();

        } 
    }

    void ActivateProjectile()
    {
        if (projectilesArray.Length == projectilesArrayIndex)
        {
            projectilesArrayIndex = 0;
        }

        //Debug.Log("1 -- INDEX-> " + projectilesArrayIndex);


        projectilesArray[projectilesArrayIndex].SetActive(true);
        GameObject proj = projectilesArray[projectilesArrayIndex];
        if(AudioManagerScript.Instance != null && shootingAudio!=null)
        {
            AudioManagerScript.Instance.PlayEffect(shootingAudio);
        }

        proj.transform.position = transform.position;
        proj.transform.rotation = transform.rotation;
        //proj.transform.localEulerAngles = desiredRotationProjectile;

        shootingTimer = 0f;

        projectilesArrayIndex++;

        //Debug.Log("2 -- FINISHED ACTIVATING PROJ");
    }
}
