using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AimAtMouse()
    {
        Vector3 mouseWorldpPositionWithZ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition = mouseWorldpPositionWithZ;
        mousePosition.z = 0f;

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0, angle);
    }
}
