using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance {  get; private set; }

    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
        EnemySpawnerController.onEnemyDestroy += UpdateScore;
    }

    private void OnDestroy()
    {
        EnemySpawnerController.onEnemyDestroy -= UpdateScore;
    }

    void UpdateScore()
    {
        scoreText.SetText(GameManager.Instance.GetScore().ToString());
    }


}
