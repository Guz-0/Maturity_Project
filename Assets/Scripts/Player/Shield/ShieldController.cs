using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShieldController : MonoBehaviour
{

    [SerializeField] private int shieldDuration;
    [SerializeField] private GameObject shieldCountdownObject;
    private TMP_Text shieldCountdownText;

    private int elapsedTime;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UnloadingShield()
    {
        elapsedTime = 0;
        shieldCountdownObject.SetActive(true);
        shieldCountdownText = shieldCountdownObject.GetComponent<TMP_Text>();

        while(elapsedTime != shieldDuration)
        {
            shieldCountdownText.SetText((shieldDuration - elapsedTime).ToString());
            elapsedTime++;
            yield return new WaitForSeconds(1f);
        }

        shieldCountdownObject.SetActive(false);
        gameObject.SetActive(false);

        yield return null;
    }

    private void OnEnable()
    {
        StartCoroutine(UnloadingShield());
    }
}
