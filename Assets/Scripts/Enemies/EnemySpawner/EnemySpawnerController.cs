using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Pool;
using System;

public class EnemySpawnerController : MonoBehaviour
{
    
    [SerializeField] private GameObject[] spawnersArray;
    [SerializeField] private GameObject enemyObject;


    private GameObject[] enemyPool = new GameObject[1000];
    private bool isThereAnyObjectDisabled = false;
    private int idxOfDisabledObject;
    private int totalEnemiesInPoolCounter = 0;

    [SerializeField] private int nextWaveScore = 5000;
    [SerializeField] private float firstSpawningCountdown = 3f;
    private int scoreElapsed = 0;
    [SerializeField] private float toSubtractFromSpawningCountdown;
    [SerializeField] private float pauseTimeBetweenRounds = 2f;


    private void Start()
    {
        EnemyController.onEnemyDestroy += EnemyGotHit;

        StartCoroutine(SpawningManager());

        /*
        InvokeRepeating(nameof(SpawnEnemy), 1, 3);
        InvokeRepeating(nameof(SpawnEnemy), 1, 3);
        InvokeRepeating(nameof(SpawnEnemy), 1, 3);
        InvokeRepeating(nameof(SpawnEnemy), 1, 3);
        */
    }

    private void OnDestroy()
    {
        EnemyController.onEnemyDestroy -= EnemyGotHit;
    }



    public void EnemyGotHit()
    {
        scoreElapsed += 100;
        Debug.Log("ENEMY GOT HIT");
    }

    public static void DisableEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
    }

    private Vector2 RandomSpawnPosition()
    {
        Collider2D myCollider = spawnersArray[UnityEngine.Random.Range(0,4)].GetComponent<Collider2D>();
        Vector2 spawnPos = new Vector2(UnityEngine.Random.Range(myCollider.bounds.max.x, myCollider.bounds.min.x), UnityEngine.Random.Range(myCollider.bounds.max.y, myCollider.bounds.min.y));
        return spawnPos;
        
    }

    public void SpawnEnemy()
    {
        GameObject disabledEnemy;
        GameObject instantiatedEnemy;
        bool flag = false;
        isThereAnyObjectDisabled = false;

        //Debug.Log("ARRAY LENGHT -> " + enemyPool.Length);
        for (int i=0; i < totalEnemiesInPoolCounter; i++)
        {
            //Debug.Log("total: " + totalEnemiesInPoolCounter);
            if (enemyPool[i] != null)
            {
                //Debug.Log("OBJECT " + i + " IS ALIVE");
                flag = enemyPool[i].activeSelf;
                //Debug.Log("OBJECT " + i + " IS " + flag);
                if (!flag)
                {
                    //Debug.Log("OBJECT " + i + " IS DISABLED");
                    flag = true;
                    isThereAnyObjectDisabled = flag;
                    idxOfDisabledObject = i;
                    break;
                }
            }
        }

        //Debug.Log("Are there objects disabled: " + isThereAnyObjectDisabled);
        if (isThereAnyObjectDisabled)
        {
            disabledEnemy = enemyPool[idxOfDisabledObject];

            disabledEnemy.SetActive(true);
            disabledEnemy.transform.position = RandomSpawnPosition();
        }
        else
        {
            
            instantiatedEnemy = Instantiate(enemyObject, RandomSpawnPosition(), transform.rotation);
            
            enemyPool[totalEnemiesInPoolCounter] = instantiatedEnemy;
            totalEnemiesInPoolCounter++;
        }
        //Debug.Log("total: " + totalEnemiesInPoolCounter);
    }

    IEnumerator SpawningManager()
    {
        while (true)
        {
            if (scoreElapsed >= nextWaveScore && firstSpawningCountdown>=1f)
            {
                firstSpawningCountdown -= toSubtractFromSpawningCountdown;
                scoreElapsed = 0;
                Debug.Log("SpawningCountdown-> " + firstSpawningCountdown);

                yield return new WaitForSeconds(pauseTimeBetweenRounds);
            }


            Debug.Log("SPAWNING ENEMIES");
            for(int i = 0; i < 4; i++)
            {
                SpawnEnemy();

            }
            yield return new WaitForSeconds(firstSpawningCountdown);
        }
    }




}
