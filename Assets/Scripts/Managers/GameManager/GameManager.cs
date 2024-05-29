using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score;
    public int bombsNumber;
    public int lifesNumber;
    public bool isPlayerAlive;

    public bool isButtonFirstTimeEnabled = true;



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
    }

    private void Start()
    {
        score = 0;
        bombsNumber = 0;
    }

    public void AddScore(int increment)
    {
        score += increment;
        //Debug.Log("Score-> " + score);
    }

    public void SetScore(int scoreValue)
    {
        score = scoreValue;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetBombsNumber()
    {
        return bombsNumber;
    }

    public void SetBombsNumber(int value)
    {
        bombsNumber = value;
    }

    public int GetLifesNumber()
    {
        return lifesNumber;
    }

    public void SetLifesNumber(int value)
    {
        lifesNumber = value;
    }

    public bool GetIsPlayerAlive()
    {
        return isPlayerAlive;
    }

    public void SetIsPlayerAlive(bool value)
    {
        isPlayerAlive = value;
    }

    public void ButtonAlreadyEnabled()
    {
        isButtonFirstTimeEnabled = false;
    }

    public bool GetisButtonFirstTimeEnabled()
    {
        return isButtonFirstTimeEnabled;
    }

}
