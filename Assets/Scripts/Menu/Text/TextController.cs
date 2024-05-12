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

    [SerializeField] private Button myButton;

    private void Start()
    {
        myButton = GetComponent<Button>();
        StartCoroutine(TypeWriter(stringToPrint.ToCharArray()));
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
            myButton.enabled = true;
        }
        yield return null;
    }
}
