using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class communication : MonoBehaviour
{

    private string param = "name";
    private TMP_Text username;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {

        Debug.Log(this.param);
        this.username = GetComponent<TMP_Text>();

        //absoluteURL è di sola lettura quindi questa operazione non si può eseguire 

        //Application.absoluteURL. = "http://example.com/?name=ciao"

        try
        {
            Debug.Log("sono nel try");
            this.param = Application.absoluteURL.Substring(Application.absoluteURL.IndexOf("=") + 1);
            Debug.Log("il nome è: " + param);

        }
        catch (System.Exception ex)
        {

            //per qualche motivo tutt'ora ignoto non arriva mai qua
            Debug.LogException(ex);
            Debug.Log("nessun parametro trovato");
            this.param = "nameError";

        }

        this.username.text = this.param;
        
    }

}
