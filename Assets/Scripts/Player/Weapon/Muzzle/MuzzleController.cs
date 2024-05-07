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

    public GameObject[] projectilesArray;
    private int projectilesArrayIndex = 0;

    void Start()
    {
        projectileBody = projectile.GetComponent<Rigidbody2D>();
        
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

        proj.transform.position = transform.position;
        proj.transform.rotation = transform.rotation;

        shootingTimer = 0f;

        projectilesArrayIndex++;

        //Debug.Log("2 -- FINISHED ACTIVATING PROJ");
    }
}
