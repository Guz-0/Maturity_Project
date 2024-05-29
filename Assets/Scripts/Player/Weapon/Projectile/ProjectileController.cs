using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    private Rigidbody2D body;
    [SerializeField] private float projectileSpeed = 1f;

    [SerializeField] private SpriteRenderer spriteRenderer;


    void Awake()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        PointToMouse();
    }

    private void Start()
    {
        //MuzzleController.OnShooting += ()=> StartCoroutine(Moving());
    }

    private void OnDestroy()
    {
        //MuzzleController.OnShooting -= () => StartCoroutine(Moving());
    }


    void Update()
    {
        CheckIfOutOfCamera();
    }

    void PointToMouse()
    {
        Vector3 mouseWorldpPositionWithZ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition = mouseWorldpPositionWithZ;
        mousePosition.z = 0f;

        Vector3 direction = (mousePosition - transform.position).normalized;
        //Debug.Log(direction.ToString());
        body.velocity = new Vector2(direction.x, direction.y) * projectileSpeed;
    }



    void CheckIfOutOfCamera()
    {
        if(spriteRenderer.isVisible == false)
        {
            //body.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        yield return new WaitForSeconds(0.01f);
        PointToMouse();
        yield return null;
    }

    



}
