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

    public static event Action onEnemyDestroy;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1, 1);
        InvokeRepeating(nameof(SpawnEnemy), 1, 1);
        InvokeRepeating(nameof(SpawnEnemy), 1, 1);
        InvokeRepeating(nameof(SpawnEnemy), 1, 1);
    }



    public static void DisableEnemy(GameObject enemy, int valueOfEnemy)
    {
        if(GameManager.Instance != null)
        {
            onEnemyDestroy.Invoke();
            GameManager.Instance?.AddScore(valueOfEnemy);
        }
        else
        {
            Debug.Log("[GAMEMANAGER IS NULL]");
        }
        
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



}
