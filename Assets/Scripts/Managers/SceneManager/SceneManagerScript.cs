using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{

    public static SceneManagerScript Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }


    public void PlayGame()
    {
        //DOTween.Clear();
        SceneManager.LoadScene("Game");
    }

    public void PlayScene(string sceneName)
    {
        GameManager.Instance.SetScore(0);
        SceneManager.LoadScene(sceneName);
    }
}
