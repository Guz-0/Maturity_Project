using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance {  get; private set; }

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text lifesText;
    [SerializeField] private TMP_Text bombsText;

    public bool isGamePaused = false;
    [SerializeField] private GameObject pauseMenuObject;
    [SerializeField] private GameObject diedMenuObject;

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
        EnemyController.onEnemyDestroy += UpdateScore;
        PlayerController.onBombDeploy += UpdateBombsNumber;
        PlayerController.onLifeLost += UpdateLifesNumber;
        PlayerController.onPlayerDeath += ActivateDiedMenu;
    }

    private void Update()
    {
        PauseSystem();
    }

    private void OnDestroy()
    {
        EnemyController.onEnemyDestroy -= UpdateScore;
        PlayerController.onBombDeploy -= UpdateBombsNumber;
        PlayerController.onLifeLost -= UpdateLifesNumber;
        PlayerController.onPlayerDeath -= ActivateDiedMenu;
    }

    void UpdateScore()
    {
        if(GameManager.Instance != null)
        {
            scoreText.SetText(GameManager.Instance.GetScore().ToString());
            //Debug.Log("UPDATING SCORE: " + GameManager.Instance.GetScore().ToString());
        }
    }

    void UpdateBombsNumber()
    {
        if (GameManager.Instance != null)
        {
            bombsText.SetText(GameManager.Instance.GetBombsNumber().ToString());
            //Debug.Log("UPDATING BOMB: " + GameManager.Instance.GetBombsNumber().ToString());
        }
    }

    void UpdateLifesNumber()
    {
        if( GameManager.Instance != null)
        {
            lifesText.SetText(GameManager.Instance.GetLifesNumber().ToString());
        }
    }

    public void PauseSystem()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.GetIsPlayerAlive())
        {
            isGamePaused = !isGamePaused;
            if (isGamePaused)
            {
                pauseMenuObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
                pauseMenuObject.SetActive(false);
            }
        }
    }

    void ActivateDiedMenu()
    {
        diedMenuObject.SetActive(true);
        Time.timeScale = 0;
    }


}
