using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendPoints : MonoBehaviour
{


    public void OnClick()
    {

        string points = "100";
        byte[] data = System.Text.Encoding.UTF8.GetBytes(points);

        UnityWebRequest www = UnityWebRequest.Put("https://www.localhost.com/htdocs/maturity_project", data);

        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);

        }
        else
        {
            Debug.Log("Dato trasmesso con successo");

        }

    }

}
