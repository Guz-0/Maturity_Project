using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class communication : MonoBehaviour
{

    private string param;
    private TMP_Text name;

    // Start is called before the first frame update
    void Start()
    {
        name = GetComponent<TMP_Text>();

        try
        {
            param = Application.absoluteURL.Substring(Application.absoluteURL.IndexOf("=") + 1);

        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
            param = "me";

        }

        //name.text = param;
        name.text = "me";

    }

}
