using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class GeneralButtonController : MonoBehaviour
{
    private Button myButton;
    [SerializeField] private string sceneName;
    [SerializeField] private bool hasToUnpause;
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
    }

    
    void Update()
    {
        
    }

    void TaskOnClick()
    {
        if (hasToUnpause)
        {
            Time.timeScale = 1f;
        }
        SceneManagerScript.Instance.PlayScene(sceneName);
    }
    
    
    

}
