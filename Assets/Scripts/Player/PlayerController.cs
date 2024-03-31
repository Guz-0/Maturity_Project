using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D body;

    [SerializeField] private float movementSpeed = 1f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Movement();
        AimAtMouse();
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal") * 100 * movementSpeed * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * 100 * movementSpeed * Time.deltaTime;

        body.velocity = new Vector3(x, y, 0);

    }

    void AimAtMouse()
    {
        Vector3 mouseWorldpPositionWithZ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition = mouseWorldpPositionWithZ;
        mousePosition.z = 0f;

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle + 90);
    }
}
