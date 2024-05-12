using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private GameObject player;

    private float movementSpeed = 1f;
    private float step;

    private int valueOfEnemy = 100;

    [SerializeField] private float minSpeed = 5;
    [SerializeField] private float maxSpeed = 20;

    [SerializeField] private AudioClip explosionAudio;

    private SpriteRenderer myRenderer;
    private Collider2D myCollider;

    private float yScaleStart;
    private int timeDivider;

    private bool didEnemyGotHit = false;
    [SerializeField] private GameObject explosionParticles;
    [SerializeField] private float explosionDuration = 4f;

    [SerializeField] private TMP_Text valueText;
    [SerializeField] private float waitBeforeFadingText = 0.5f;
    [SerializeField] private float fadingTime = 1f;

    private Vector3 firstScale;

    private void Awake()
    {
        firstScale = transform.localScale;
        Debug.Log(firstScale);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PLAYER");
        yScaleStart = gameObject.transform.localScale.y;

        myRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();

        valueText.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!didEnemyGotHit)
        {
            FollowPlayer();
        }
    }

    private void Update()
    {
        if (!didEnemyGotHit)
        {
            ChangeScale();
        }
    }

   

    void ChangeScale()
    {

        float varScaleY = Mathf.PingPong(Time.time/timeDivider, yScaleStart) + 0.2f;

        transform.localScale = new Vector3(transform.localScale.x, varScaleY, transform.localScale.z);
    }

    void FollowPlayer()
    {
        //AimAtPlayer();

        
        step = Time.deltaTime * movementSpeed;
        transform.position = Vector2.MoveTowards(transform.position,player.transform.position, step);
    }

    void AimAtPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        playerPosition.z = 0f;

        Vector3 aimDirection = (playerPosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PROJECTILE"))
        {
            myCollider.enabled = false;
            StartCoroutine(AfterEnemyDeath());
        }
    }

    IEnumerator AfterEnemyDeath()
    {
        if(AudioManagerScript.Instance != null)
        {
            AudioManagerScript.Instance.PlayEffect(explosionAudio);
        }
        didEnemyGotHit = true;


        myRenderer.enabled = false;
        transform.localScale = new Vector3(transform.localScale.x, firstScale.y, transform.localScale.z);
        Debug.Log("new scale: " + transform.localScale);
        explosionParticles.SetActive(true);

        valueText.enabled = true;

        valueText.SetText("+" + valueOfEnemy);

        yield return new WaitForSeconds(waitBeforeFadingText);

        valueText.DOFade(0, fadingTime);

        yield return new WaitForSeconds(explosionDuration);

        explosionParticles.SetActive(false);
        valueText.DOFade(1, 0f);
        valueText.enabled = false;
        EnemySpawnerController.DisableEnemy(gameObject,valueOfEnemy);   

        yield return null;
    }

    private void OnEnable()
    {
        timeDivider = Random.Range(5, 11);
        movementSpeed = Random.Range(minSpeed, maxSpeed);

        
        didEnemyGotHit = false;
        if (myRenderer != null && myCollider!=null)
        {
            myRenderer.enabled = true;
            myCollider.enabled = true;
        }
        explosionParticles.SetActive(false);
    }


}
