using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class sendPoints : MonoBehaviour
{

    private void Start()
    {
        PlayerController.onPlayerDeath += sendRequest;
    }

    private void OnDestroy()
    {
        PlayerController.onPlayerDeath -= sendRequest;
    }

    void sendRequest()
    {
        
        byte[] data = System.Text.Encoding.UTF8.GetBytes(GameManager.Instance.GetScore().ToString());
        UnityWebRequest www = UnityWebRequest.Put("http://htdocs/maturity_project/", data);

        if(www.result != UnityWebRequest.Result.Success) 
        {
            Debug.Log(www.error);

        }else
        {
            Debug.Log("trasmissione avvenuta con successo");

        }





    }

}
