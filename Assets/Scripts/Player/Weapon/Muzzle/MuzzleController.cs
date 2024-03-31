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

            Instantiate(projectile, transform.position, transform.rotation);
            shootingTimer = 0f;

        }
        

    }
}
