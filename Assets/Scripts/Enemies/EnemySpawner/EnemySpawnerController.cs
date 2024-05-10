using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Pool;

public class EnemySpawnerController : MonoBehaviour
{
    
    [SerializeField] private GameObject[] spawnersArray;
    [SerializeField] private GameObject enemyObject;


    private GameObject[] enemyPool = new GameObject[100];
    private bool isThereAnyObjectDisabled = false;
    private int idxOfDisabledObject;
    private int totalEnemiesInPoolCounter = 0;



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
            GameManager.Instance.AddScore(valueOfEnemy);
        }
        else
        {
            Debug.Log("[GAMEMANAGER IS NULL]");
        }
        enemy.SetActive(false);
    }

    private Vector2 RandomSpawnPosition()
    {
        Collider2D myCollider = spawnersArray[Random.Range(0,3)].GetComponent<Collider2D>();
        Vector2 spawnPos = new Vector2(Random.Range(myCollider.bounds.max.x, myCollider.bounds.min.x), Random.Range(myCollider.bounds.max.y, myCollider.bounds.min.y));
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
