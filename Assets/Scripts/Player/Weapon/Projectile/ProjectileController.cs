using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    private Rigidbody2D body;
    [SerializeField] private float projectileSpeed = 1f;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();

        Vector3 mouseWorldpPositionWithZ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition = mouseWorldpPositionWithZ;
        mousePosition.z = 0f;

        Vector3 direction = (mousePosition - transform.position).normalized;
        body.velocity = new Vector2(direction.x, direction.y) * projectileSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
