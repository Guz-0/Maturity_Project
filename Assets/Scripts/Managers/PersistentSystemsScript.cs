using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSystemsScript : MonoBehaviour
{

    public static PersistentSystemsScript Instance {  get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}