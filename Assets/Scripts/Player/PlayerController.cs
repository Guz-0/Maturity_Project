using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D body;

    [SerializeField] private float movementSpeed = 1f;

    private Material myMaterial;

    private float dissolveAmount;
    [SerializeField] private float stepDissolveAnimation = 0.05f;

    [SerializeField] private GameObject exhaustObject;
    [SerializeField] private GameObject bombObject;
    [SerializeField] private GameObject shieldObject;

    private int bombsNumber;

    public static event Action onBombDeploy;
    public static event Action onLifeLost;
    public static event Action onPlayerDeath;

    private int lifesNumber;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        myMaterial = GetComponent<SpriteRenderer>().material;
        myMaterial.SetFloat("_DissolveAmount", 1f);

        dissolveAmount = myMaterial.GetFloat("_DissolveAmount");
        StartCoroutine(DissolveAnimation());
        //Debug.Log("player");

        bombsNumber = 5;
        lifesNumber = 3;
    }

    void Update()
    {
        CheckBomb();
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

    void CheckBomb()
    {
        if ((bombsNumber != 0) && (Input.GetKeyDown(KeyCode.Space)) && (bombObject.activeSelf != true))
        {
            bombObject.SetActive(true);
            bombsNumber--;
            if ((GameManager.Instance != null))
            {
                GameManager.Instance.SetBombsNumber(bombsNumber);
            }
            onBombDeploy.Invoke();

        }
    }

    IEnumerator DissolveAnimation()
    {
        while (dissolveAmount > 0.01f)
        {
            dissolveAmount -= stepDissolveAnimation;
            if (dissolveAmount < 0.01f)
            {
                myMaterial.SetFloat("_DissolveAmount", 0f);
                break;
            }
            else
            {
                myMaterial.SetFloat("_DissolveAmount", dissolveAmount);
            }

            //Debug.Log(myMaterial.GetFloat("_DissolveAmount"));


            yield return new WaitForFixedUpdate();
        }

        exhaustObject.SetActive(true);

        yield return null;
    }

   
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ENEMY") && (shieldObject.activeSelf == false))
        {
            shieldObject.SetActive(true);
            lifesNumber--;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SetLifesNumber(lifesNumber);
            }
            onLifeLost.Invoke();
            if (lifesNumber == 0)
            {
                Debug.Log("000LifesNumber->" + lifesNumber);
                OnPlayerDeathFunction();
            }
            Debug.Log("LifesNumber->" + lifesNumber);
        }
    }

    void OnPlayerDeathFunction()
    {
        onPlayerDeath.Invoke();
        gameObject.SetActive(false);
    }


}
