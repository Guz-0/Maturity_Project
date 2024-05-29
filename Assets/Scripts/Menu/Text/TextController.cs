using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private string wordToPrint;
    private char[] arrayWordToPrint;

    [SerializeField] private float waitingTime = 0.1f;

    [SerializeField] private float maxTime;
    [SerializeField] private float minTime;

    [SerializeField] private string stringToPrint;

    private Button myButton;
    private bool isButtonFirstTimeEnabled;

    private bool isAnimationFinished = false;

    /// <summary>
    /// 
    /// </summary>
    /// 

    [Header("Enter-Exit animations")]

    [SerializeField] private float scaleOffsetMultiplier = 1.5f;
    [SerializeField] private float scaleAnimationDuration = 0.5f;

    [SerializeField] private float yOffset = 10f;
    [SerializeField] private float yAnimationDuration = 0.5f;

    private Vector3 endScale;
    private Vector3 fromScale;

    private float endYPosition;
    private float fromYPosition;

    //[SerializeField] private GameObject rightImage;
    //[SerializeField] private GameObject leftImage;

    private void Awake()
    {
        if(GameManager.Instance != null)
        {
            isButtonFirstTimeEnabled = GameManager.Instance.GetisButtonFirstTimeEnabled();
            GameManager.Instance.ButtonAlreadyEnabled();
        }
        
        myButton = GetComponent<Button>();
    }

    private void Start()
    {
        endScale = transform.localScale * scaleOffsetMultiplier;
        fromScale = transform.localScale;

        endYPosition = transform.position.y + yOffset;
        fromYPosition = transform.position.y;
    }

    private void OnEnable()
    {
        if (isButtonFirstTimeEnabled)
        {
            //StartCoroutine(TypeWriter(stringToPrint.ToCharArray()));
            //myButton.enabled = false;
            isButtonFirstTimeEnabled = false;
        }
        else
        {
            text.SetText(stringToPrint);
            isAnimationFinished = true;
        }
        Debug.Log("ONENABLE animation: " + isAnimationFinished + "\nbuttonFirstTime: " + isButtonFirstTimeEnabled);
    }

    private void OnDisable()
    {

        /*if(rightImage.activeSelf && leftImage.activeSelf)
        {
            rightImage.SetActive(false);
            leftImage.SetActive(false);

        }*/

        //OnPointerExit();
    }

    IEnumerator TypeWriter(char[] array)
    {
        bool flag = false;
        int randomAscii;
        char randomChar;
        string finalString = "";
        float expiredTime = 0;
        float maxT = 0;

        
        for (int i = 0; i < array.Length; i++)
        {
            flag = false;
            maxT = UnityEngine.Random.Range(minTime, maxTime);
            expiredTime = 0f;
            Debug.Log("NEXT LETTER");
            for (int j = 0; !flag; j++)
            {
                expiredTime += Time.deltaTime + waitingTime;
                //Debug.Log(expiredTime);
                randomAscii = UnityEngine.Random.Range(65, 123);
                randomChar = Convert.ToChar(randomAscii);

                text.SetText(finalString + randomChar);

                flag = (randomChar == array[i]);
                if (flag)
                {
                    //Debug.Log("FOUND LETTER --> " + randomChar);
                    finalString += randomChar;
                }
                else if (expiredTime >= maxT)
                {
                    //Debug.Log("TIME EXPIRED, LETTER IS -->  " + array[i]);
                    finalString += array[i];
                    text.SetText(finalString);
                    flag = true;
                }

                yield return new WaitForSeconds(waitingTime);
            }
        }

        

        if (myButton != null)
        {
            isAnimationFinished = true;
            myButton.enabled = true;
        }

        Debug.Log("AFTER ANIMATION animation: " + isAnimationFinished + "\nbuttonFirstTime: " + isButtonFirstTimeEnabled);

        yield return null;
    }

    /*

    public void OnPointerEnter()
    {
        if (isAnimationFinished)
        {
            transform.DOMoveY(endYPosition, yAnimationDuration);
            transform.DOScale(endScale, scaleAnimationDuration);

            //leftImage.SetActive(true);
            //rightImage.SetActive(true);
        }
    }

    public void OnPointerExit()
    {
        if (isAnimationFinished)
        {
            transform.DOMoveY(fromYPosition, yAnimationDuration);
            transform.DOScale(fromScale, scaleAnimationDuration);
            
            //leftImage.SetActive(false);
            //rightImage.SetActive(false);
        }
    }

    */
}
