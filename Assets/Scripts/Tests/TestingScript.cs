using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;
using System;

public class TestingScript : MonoBehaviour
{
    public GameObject spawner;
    private Collider2D myCollider;
    private Vector2 x;
    private Vector2 y;

    [SerializeField] private TMP_Text text;

    private string wordToPrint;
    private char[] arrayWordToPrint;

    [SerializeField] private float waitingTime = 0.1f;

    public float maxTime;
    public float minTime;

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ColliderTest()
    {
        x = myCollider.bounds.max;
        y = myCollider.bounds.min;
        //Debug.Log("MAX -> " + x + "\nMIN -> " + y);
    }

    public void Testing()
    {
        /*int i = 0;
        wordToPrint = "play";
        arrayWordToPrint = wordToPrint.ToCharArray();
        /*for(int i = 0; i < arrayWordToPrint.Length; i++)
        {
            Debug.Log(arrayWordToPrint[i]);
        }
        Debug.Log(wordToPrint.IndexOf('l'));
        Debug.Log(wordToPrint.ToIntArray());

        Encoding utf8 = Encoding.UTF8;

        int[] ascii = {UnityEngine.Random.Range(48,123), UnityEngine.Random.Range(48, 123), UnityEngine.Random.Range(48, 123) };


        for (i = 0; i < ascii.Length; i++)
        {
            Debug.Log(Convert.ToChar(ascii[i]));
        }
         48 -> 123
        */
        string word = "hello";
        char[] array = word.ToCharArray();
        StartCoroutine(TypeWriter(array));
        Debug.Log("what");

        
    }

    IEnumerator TypeWriter(char[] array)
    {
        bool flag = false;
        int randomAscii;
        char randomChar;
        string finalString = "";
        float expiredTime = 0;
        float maxT = 0;

        for(int i = 0; i < array.Length; i++)
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
                }else if (expiredTime >= maxT)
                {
                    //Debug.Log("TIME EXPIRED, LETTER IS -->  " + array[i]);
                    finalString += array[i];
                    text.SetText(finalString);
                    flag = true;
                }

                yield return new WaitForSeconds(waitingTime);
            }
        }

        yield return null;
    }
}
