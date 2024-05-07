using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class communication : MonoBehaviour
{

    private string param;                               //variable used to contain the name                           
    private TMP_Text username;                          //reference to the GameObject that shows the name

    private void Awake()
    {

        this.username = GetComponent<TMP_Text>();
        

        if(Application.absoluteURL.Length < 1)              //if the variable is empty set a default name
        {
            Debug.Log("nessun parametro trovato");
            this.param = "nameError";                       //default name

        }else {                                             //if it isn't empty read from the variable absoluteURL

            this.param = Application.absoluteURL.Substring(Application.absoluteURL.IndexOf("=") + 1);   //initiates param to the first URL parameter, that occurs after the first '='
            Debug.Log("il nome è: " + param);

        }

        this.username.text = this.param;                    //inits the text variable of the gameObject "Username" to the param                 
        
    }

}
